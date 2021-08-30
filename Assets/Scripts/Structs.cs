using System;
using UnityEngine;
using Ziggurat.Units;

namespace Ziggurat
{
    /// <summary>
    /// Данные статуса
    /// </summary>
    [Serializable]
    //public class StatusDataArgs
    //{
    //    /// <summary>
    //    /// Название статуса
    //    /// </summary>
    //    public string Name { get; }
    //    /// <summary>
    //    /// Длительность в секундах
    //    /// </summary>
    //    public float Duration { get; }
    //    /// <summary>
    //    /// Целевой объект статуса
    //    /// </summary>
    //    public BaseUnit Target { get; }
    //    /// <summary>
    //    /// Источник статуса
    //    /// </summary>
    //    public BaseUnit Source { get; }
    //    /// <summary>
    //    /// Количество циклов
    //    /// </summary>
    //    public int Count;
    //    /// <summary>
    //    /// Тип статуса
    //    /// </summary>
    //    public StatusType Type { get; }

    //    /// <summary>
    //    /// Текущее оставшееся время действия статуса
    //    /// </summary>
    //    public float CurrentDuration { get; set; }

    //    public StatusDataArgs(string name, float duraction, BaseUnit target, BaseUnit source, StatusType type)
    //    {
    //        Name = name; Duration = CurrentDuration = duraction; Target = target; Source = source; Type = type;
    //    }
    //}
    public struct WeightData
    {
        public ActionType[] BotActions;
        public ActionResultType[] Results;
        public float Coefficient;
    }

    /// <summary>
    /// Структура, описывающая результат действия юнита
    /// </summary>
    public readonly struct ActionResultInfo
    {
        public readonly ActionType Type;
        public readonly IUnit Source;
        public readonly IUnit Target;
        public readonly ActionResultType Result;

        public ActionResultInfo(ActionType type, IUnit source, IUnit target, ActionResultType result)
        {
            Type = type; Source = source; Target = target; Result = result;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ActionResultInfo)) return false;

            var str = (ActionResultInfo)obj;

            return str.Type == Type &&
                str.Source.Equals(Source) &&
                str.Target.Equals(Target) &&
                str.Result == Result;
        }
    }

    [Serializable]
    public struct MovementData
    {
        [Tooltip("Максимальная сила смещения"), Range(0f, 100f)]
        public float MaxVelocity;
        [Tooltip("Максимальная скорость перемещения"), Range(0f, 100f)]
        public float Speed;
        [Tooltip("Радиус прибытия к цели"), Range(0f, 100f)]
        public float SqrDistanceArrival;
    }
    public readonly struct UnitActionData
    {
        public readonly string Key;
        public readonly Interval<float> Interval;

        public UnitActionData(string key, float minDistance = -1f, float maxDistance = -1f)
        {
            Key = key; Interval = new Interval<float>(minDistance, maxDistance);
        }
        public UnitActionData(string key, Interval<float> interval)
        {
            Key = key; Interval = interval;
        }
    }

    /// <summary>
    /// Точка в пространстве
    /// </summary>
    [Serializable]
    public readonly struct TargetPoint
    {
        private readonly Transform _transform;
        private readonly Vector3 _position;

        public readonly IUnit Target;
        public Vector3 Position
        {
            get
            {
                if (_transform != null) return _transform.position;
                if (_position != null) return _position;
                return Target.Position;
            }
        }

        public TargetPoint(IUnit unit)
        {
            Target = unit;
            _position = Vector3.zero;
            _transform = null;
        }
        public TargetPoint(Transform transform)
        {
            Target = null;
            _position = Vector3.zero;
            _transform = transform;
        }
        public TargetPoint(Vector3 position)
        {
            Target = null;
            _position = position;
            _transform = null;
        }

        /// <summary>
        /// Неопределенное значение структуры по-умолчанию
        /// </summary>
        public static readonly TargetPoint Indefinite =
            new TargetPoint(new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity));

        public override bool Equals(object obj)
        {
            if (obj is TargetPoint point)
            {
                if (point.Position == Position && point.Target == Target) return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(TargetPoint a, TargetPoint b)
        {
            return a.Position == b.Position && a.Target == b.Target;
        }
        public static bool operator !=(TargetPoint a, TargetPoint b)
        {
            return a.Position != b.Position || a.Target != b.Target;
        }
    }

    public struct Interval<T> where T : struct
    {
        public static readonly Interval<float> FloatIndefinite = new Interval<float>(-1f, -1f);
        public static readonly Interval<int> IntIndefinite = new Interval<int>(-1, -1);

        public T Min;
        public T Max;

        public Interval(T min, T max)
        {
            Min = min; Max = max;
        }

        public override bool Equals(object obj)
        {
            var type = obj.GetType();

            //Получаем информацию о полях
            var minField = type.GetField("Min");
            if (minField == null) return false;
            var maxField = type.GetField("Max");
            if (maxField == null) return false;

            //Получаем из сущности значение ее полей
            var value1 = minField.GetValue(obj);
            var value2 = maxField.GetValue(obj);
            //Если : тип наших полей не соответствует типу полей сущности или типы полей сущностей не одинаковы - она нам не идентична
            if (value1.GetType() != typeof(T) || value1.GetType() == value2.GetType()) return false;
            //
            return value1.Equals(Min) && value2.Equals(Max);

            //Не проходят: классы, не шаблоны и примитивы
            //if (!type.IsValueType || !type.IsGenericType || type.IsPrimitive) return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"Min: {Min} Max: {Max}";
        }

        public static bool operator ==(Interval<T> a, Interval<T> b)
        {
            return a.Min.Equals(b.Min) && a.Max.Equals(b.Max);
        }
        public static bool operator !=(Interval<T> a, Interval<T> b)
        {
            return !a.Min.Equals(b.Min) || !a.Max.Equals(b.Max);
        }
    }
}