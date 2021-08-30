using System;
using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ziggurat.AI
{
    public static class AIUtility
    {
        private static readonly string c_ConfigPath = "//Resources//Config.xml";

        private static Dictionary<DistanceType, List<ActionType>> _distance = new Dictionary<DistanceType, List<ActionType>>();
        private static Dictionary<UnitType, string[]> _animationKeys { set; get; } = new Dictionary<UnitType, string[]>();
        private static List<WeightData> _playerCoefs = new List<WeightData>();

        // Запускается автоматически
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Configuration()
        {
            try
            {
                var root = XDocument.Load(Application.dataPath + c_ConfigPath).Root;
                //Проходка по всем условиям машины анимации
                foreach (var pair in root.Element("Units").Elements("Unit"))
                {
                    var type = (UnitType)Enum.Parse(typeof(UnitType), pair.Attribute("Type").Value);
                    var actions = pair.Attribute("Actions").Value.Split(' ');
                    _animationKeys.Add(type, actions);
                }
            }
            //Обработка исключения
            catch (Exception e)
            {
                Debug.LogError(e);

#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
                throw e;
            }
        }

        /// <summary>
        /// Возвращает словарь анимационных ключей, связанных с действиями
        /// </summary>
        /// <param name="unitName">Имя юнита</param>
        /// <returns>Словарь ключей</returns>
       // public static string[] GetKeysForActionTypes(UnitType type) => _animationKeys[type];
        /// <summary>
        /// Возвращает коллекцию для чтения с коэффициентами изменения приоритетов действий бота по действиям игрока
        /// </summary>
        public static IReadOnlyList<WeightData> GetPlayerActionCoefs => _playerCoefs;
    }

    [Serializable]
    public class ActionTypeWeightDictionary : SerializableDictionaryBase<ActionType, float> { }

    [Serializable]
    public class ActionResultWeightDictionary : SerializableDictionaryBase<ActionResultType, float> { }
    public class WeightEnum<T> where T : struct
    {
        //Внутренние сущности класса
        private IReadOnlyDictionary<T, int> _keys;
        private float[] _values;

        //Индексатор, позволяет обращаться к весам по значению перечисления
        public float this[T key]
        {
            get
            {
                return _values[_keys[key]];
            }
            set
            {
                _values[_keys[key]] = value;
            }
        }

        public int Count => _keys.Count;

        public IEnumerable<T> Keys => _keys.Keys;

        public WeightEnum()
        {
            var array = Enum.GetValues(typeof(T));

            var keys = new Dictionary<T, int>(array.Length);
            _values = new float[array.Length];

            int i = 0;
            foreach (var en in array)
            {
                keys.Add((T)en, i);
                i++;
            }

            _keys = keys;
        }
        public WeightEnum(IReadOnlyDictionary<T, float> weights)
        {
            var keys = new Dictionary<T, int>(weights.Count);
            _values = new float[weights.Count];

            int i = 0;
            foreach (var pair in weights)
            {
                keys.Add(pair.Key, i);
                _values[i] = pair.Value;
                i++;
            }

            _keys = keys;
        }

        public IEnumerator<KeyAndWeightPair<T, float>> GetEnumerator()
        {
            return new GroupEnumerator(_keys, _values);
        }

        /// <summary>
        /// Попытка получить значение по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение или default значение</param>
        /// <returns>Успешно-ли получено значение по ключу</returns>
        public bool TryGetValue(T key, out float value)
        {
            var res = _keys.TryGetValue(key, out var i);
            value = res ? _values[i] : default(float);
            return res;
        }

        /// <summary>
        /// Проверяет, содержится-ли данный ключ в сущности
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Истина, если ключ найден</returns>
        public bool ContainsKey(T key)
        {
            return _keys.ContainsKey(key);
        }

        public struct KeyAndWeightPair<T1, T2> where T1 : struct where T2 : struct
        {
            public T1 Key;
            public T2 Weight;

            public KeyAndWeightPair(T1 key, T2 weight)
            {
                Key = key; Weight = weight;
            }

            //Для удобства вывода текстовой версии
            public override string ToString()
            {
                return Key + " : " + Weight;
            }

            //Для избегания рефлексии, если потребуется сравнивать пары
            public override bool Equals(object obj)
            {
                if (!(obj is KeyAndWeightPair<T1, T2>)) return false;

                var pair = (KeyAndWeightPair<T1, T2>)obj;
                return Key.Equals(pair.Key) && Weight.Equals(pair.Weight);
            }
        }

        //Внутренний класс, определяющий логику перемещения в foreach
        private class GroupEnumerator : IEnumerator<KeyAndWeightPair<T, float>>
        {
            //Индекс итерации по классу и удобное представление структуры
            private List<KeyAndWeightPair<T, float>> _list;
            private int _current = -1;

            public GroupEnumerator(IReadOnlyDictionary<T, int> keys, float[] values)
            {
                _list = new List<KeyAndWeightPair<T, float>>(keys.Count);
                foreach (var el in keys)
                {
                    _list.Add(new KeyAndWeightPair<T, float>(el.Key, values[el.Value]));
                }
            }

            /// <summary>
            /// Значение из текущей итерации.
            /// </summary>
            public KeyAndWeightPair<T, float> Current => _list[_current];

            //Возвращает значение из текущей итерации, если foreach работает с обобщеной коллекцией
            object IEnumerator.Current => Current;

            //Выделение памяти в данном классе отсутствует, очистка не требуется
            public void Dispose() { }

            //Смещает итератор на следующий элемент
            //Если успешно - итерирование продолжается
            public bool MoveNext()
            {
                if (_current < _list.Count - 1)
                {
                    _current++;
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Сбрасывает итерирование.
            /// </summary>
            public void Reset()
            {
                _current = -1;
            }
        }

        public List<KeyAndWeightPair<T, float>> ToList()
        {
            var list = new List<KeyAndWeightPair<T, float>>(_keys.Count);

            foreach (var el in this)
            {
                list.Add(el);
            }
            return list;
        }
    }

    public delegate void ActionEventHandler(ActionType newType, bool isLoop);

    public static partial class Extensions
    {
        public static Interval<float> Pow(this Interval<float> interval, float value = 2)
        {
            return new Interval<float>(Mathf.Pow(interval.Min, value), Mathf.Pow(interval.Max, value));
        }
        public static Interval<int> Pow(this Interval<int> interval, float value = 2)
        {
            return new Interval<int>(Mathf.RoundToInt(Mathf.Pow(interval.Min, value)), Mathf.RoundToInt(Mathf.Pow(interval.Max, value)));
        }

        /// <summary>
        /// Конвертирует двумерный вектор в трехмернй вектор
        /// </summary>
        /// <param name="vector">Двумерный вектор</param>
        /// <returns>Трехмерный вектор</returns>
        public static Vector3 ConvertToMoveVector3(this Vector2 vector) => new Vector3(vector.x, 0f, vector.y);
    }
}