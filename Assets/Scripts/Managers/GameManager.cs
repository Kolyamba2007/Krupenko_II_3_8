using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ziggurat.Units;

namespace Ziggurat.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager Instance => FindObjectOfType<GameManager>(); // Лучше так не делать, но пока сойдёт
        private LinkedList<BaseUnit> Units = new LinkedList<BaseUnit>();

        [SerializeField]
        private ResourcesManager _resourcesManager;
        [SerializeField]
        private Transform _unitParent;
        [SerializeField]
        private Transform _poolPoint;

        public event Action<BaseUnit> UnitCreated;
        public event Action<BaseUnit> UnitDied;

        private void Awake()
        {
            UnitDied += OnUnitDied;
        }
        private void Start()
        {
            foreach (var unit in Units)
            {
                if (unit is BaseManufacture)
                {
                    var manufacture = unit as BaseManufacture;
                    manufacture.PoolPoint = _poolPoint.position;
                    manufacture.Manufactured += (type) =>
                    {
                        var newUnit = CreateUnit(type, manufacture.SpawnPoint.position, manufacture.Owner);
                        OnUnitManufactured(newUnit, manufacture.PoolPoint);
                    };
                    manufacture.ProduceUnit<KnightScript>();
                }
            }
        }

        private void AddUnit(BaseUnit unit)
        {
            if (Units.Contains(unit)) Units.AddLast(unit);
        }
        private void RemoveUnit(BaseUnit unit)
        {
            Units.Remove(unit);
        }
        private BaseUnit FindNearestEnemy(BaseUnit unit)
        {
            if (Units.Count == 0) return null;

            BaseUnit enemy = null;
            float minDist = float.MaxValue;

            foreach (BaseUnit target in Units)
            {
                if (target == unit || target.IsAllied(unit)) continue;

                float dist = Vector3.Distance(unit.Position, target.Position);
                if (dist < minDist)
                {
                    minDist = dist;
                    enemy = target;
                }
            }
            return enemy;
        }
        private BaseUnit CreateUnit(Type type, Vector3 position, Owner owner = Owner.Neutral)
        {
            GameObject unit = null;
            BaseUnit component = null;
            try
            {
                var prefab = _resourcesManager.GetUnitByType(type);
                unit = Instantiate(prefab, position, Quaternion.identity);
                unit.transform.SetParent(_unitParent);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                if (!unit.GetComponent(type)) unit.AddComponent(type);
                component = (BaseUnit)unit.GetComponent(type);

                unit.name = component.Name;

                component.died += () => UnitDied?.Invoke(component);

                AddUnit(component);
                UnitCreated?.Invoke(component);
            }
            return component;
        }

        #region Unit Events
        private void OnUnitManufactured(BaseUnit unit, Vector3? poolPoint)
        {
            if (unit is BaseMelee melee && poolPoint.HasValue) melee.MoveTo(poolPoint.Value);
        }
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

        public static void RegisterUnit(BaseUnit unit)
        {
            if (Instance.Units.Contains(unit)) return;
            unit.died += () => Instance.UnitDied?.Invoke(unit);
            Instance.Units.AddLast(unit);
        }
    }
}