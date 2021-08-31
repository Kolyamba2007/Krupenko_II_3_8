using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ziggurat.Units;

namespace Ziggurat.Managers
{
    public class GameManager : MonoBehaviour
    {
        private LinkedList<IUnit> Units = new LinkedList<IUnit>();

        [SerializeField]
        private ResourcesManager _resourcesManager;
        [SerializeField]
        private Transform _unitParent;

        private event Action<BaseUnit> UnitDied;

        private void Awake()
        {
            UnitDied += OnUnitDied;
        }
        private void Start()
        {
            var test1 = CreateUnit<KnightScript>(new Vector3(5, 5, 5));
            var test2 = CreateUnit<KnightScript>(new Vector3(10, 5, 10));
        }

        private void AddUnit(IUnit unit)
        {
            if (Units.Contains(unit)) Units.AddLast(unit);
        }
        private void RemoveUnit(IUnit unit)
        {
            Units.Remove(unit);
        }
        private IUnit FindNearestEnemy(IUnit unit)
        {
            if (Units.Count == 0) return null;

            IUnit enemy = null;
            float minDist = float.MaxValue;

            foreach (IUnit target in Units)
            {
                if (target == unit) continue;

                float dist = Vector3.Distance(unit.Position, target.Position);
                if (dist < minDist)
                {
                    minDist = dist;
                    enemy = target;
                }
            }
            return enemy;
        }
        private GameObject CreateUnit<T>(Vector3 position) where T : BaseUnit
        {
            GameObject unit = null;
            try
            {
                var prefab = _resourcesManager.GetUnitByType<T>();
                unit = Instantiate(prefab, position, Quaternion.identity);
                unit.transform.SetParent(_unitParent);
            }
            catch (Exception e)
            {
                Debug.LogError("Ошибка загрузки префаба!");
                Debug.LogError(e);
            }
            finally
            {
                var component = unit.GetComponent<T>();
                unit.name = component.Name;

                component.died += () => UnitDied?.Invoke(component);

                AddUnit(component);
            }
            return unit;
        }

        #region Unit Events
        private void OnUnitDied(BaseUnit unit)
        {
            RemoveUnit(unit);
            StartCoroutine(DeathCoroutine(unit));
        }
        private IEnumerator DeathCoroutine(BaseUnit unit)
        {
            yield return new WaitForSeconds(10f);
            Destroy(unit.gameObject);
        }
        #endregion
    }
}