using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ziggurat.Units;
using Ziggurat.Configuration;

namespace Ziggurat.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager Instance => FindObjectOfType<GameManager>(); // Лучше так не делать, но пока сойдёт
        private static LinkedList<BaseUnit> Units = new LinkedList<BaseUnit>();

        [SerializeField]
        private StatsConfiguration _statsConfig;
        [Space, SerializeField]
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
                component.Owner = owner;
                component.died += () => UnitDied?.Invoke(component);

                unit.name = component.Name;              

                AddUnit(component);
                UnitCreated?.Invoke(component);
            }
            return component;
        }

        #region Unit Events
        private BaseUnit FindNearestEnemy(BaseUnit unit, IEnumerable<BaseUnit> list)
        {
            if (list.Count() == 0) return null;
            list.ToList().Remove(unit);

            BaseUnit enemy = null;
            float minDist = float.MaxValue;

            foreach (var target in list)
            {
                if (target.IsAllied(unit) || target.Invulnerable) continue;

                float dist = Vector3.Distance(unit.Position, target.Position);
                if (dist < minDist)
                {
                    minDist = dist;
                    enemy = target;
                }
            }
            return enemy;
        }

        private void OnUnitManufactured(BaseUnit unit, Vector3? poolPoint)
        {
            if (unit is BaseMelee melee)
            {
                var list = Units.Where(x => x is BaseMelee).ToList();
                var nearestEnemy = FindNearestEnemy(unit, list);
                if (nearestEnemy != null) melee.MoveTo(nearestEnemy);
                else melee.MoveTo(poolPoint.Value);
            }
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
            if (Units.Contains(unit)) return;
            unit.died += () => Instance.UnitDied?.Invoke(unit);
            Units.AddLast(unit);
        }
        public static IReadOnlyList<BaseUnit> GetUnits() => Units.ToList();
        public static StatsData GetStats() => Instance._statsConfig.AllProperties;
    }
}