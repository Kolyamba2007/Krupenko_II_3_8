using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ziggurat.Units;

namespace Ziggurat.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager Instance => FindObjectOfType<GameManager>();
        private LinkedList<IUnit> Units = new LinkedList<IUnit>();

        [SerializeField]
        private ResourcesManager _resourcesManager;
        [SerializeField]
        private Transform _unitParent;
        [SerializeField]
        private Transform _poolPoint;

        public event Action<IUnit> UnitCreated;
        public event Action<IUnit> UnitDied;

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
        private void OnUnitManufactured(IUnit unit, Vector3? poolPoint)
        {
            if (unit is BaseMelee melee && poolPoint.HasValue) melee.MoveTo(poolPoint.Value);
        }
        private void OnUnitDied(IUnit unit)
        {
            RemoveUnit(unit);
            StartCoroutine(DeathCoroutine(unit as BaseUnit));
        }
        private IEnumerator DeathCoroutine(BaseUnit unit)
        {
            yield return new WaitForSeconds(10f);
            Destroy(unit.gameObject);
        }
        #endregion

        public static void RegisterUnit(IUnit unit)
        {
            if (Instance.Units.Contains(unit)) return;
            unit.died += () => Instance.UnitDied?.Invoke(unit as BaseUnit);
            Instance.Units.AddLast(unit);
        }
    }
}