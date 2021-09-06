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
        private static GameManager Instance { set; get; }
        private static LinkedList<BaseUnit> Units = new LinkedList<BaseUnit>();

        private static Dictionary<Owner, Color> Colors = new Dictionary<Owner, Color>()
        {
            { Owner.Red, Color.red },
            { Owner.Green, Color.green },
            { Owner.Blue, Color.blue },
        };

        [SerializeField]
        private StatsConfiguration _statsConfig;

        [Header("Controllers")]
        [SerializeField]
        private CameraController _cameraController;

        [Header("Managers")]
        [SerializeField]
        private ResourcesManager _resourcesManager;
        [SerializeField]
        private UIManager _UIManager;

        [Space, SerializeField]
        private ZigguratScript[] Ziggurats;
        [SerializeField]
        private Transform _unitParent;
        [SerializeField]
        private Transform _poolPoint;

        public event Action<BaseUnit> UnitCreated;
        public event Action<BaseUnit> UnitDied;

        public static BaseUnit SelectedUnit;

        [SerializeField]
        private Animator panelAnim;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            _UIManager.Init(_cameraController);

            UnitDied += OnUnitDied;
        }
        private void Start()
        {
            foreach (var unit in Ziggurats)
            {
                unit.PoolPoint = _poolPoint.position;
                unit.Manufactured += (type) =>
                {
                    var newUnit = CreateUnit(type, unit.SpawnPoint.position, unit.Owner);
                    newUnit.SetParams(unit.GetStats());
                    OnUnitManufactured(newUnit, unit.PoolPoint);
                };
            }
        }
        private void Update()
        {
            foreach (var unit in Ziggurats)
            {
                if (!unit.IsManufacturing) unit.ProduceUnit<KnightScript>();
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
                component.SetColor(Colors[owner]);
                component.died += () => UnitDied?.Invoke(component);

                unit.name = component.Name;              

                AddUnit(component);
                UnitCreated?.Invoke(component);
            }
            return component;
        }

        #region Unit Events  
        private void OnUnitManufactured(BaseUnit unit, Vector3? poolPoint)
        {
            if (unit is BaseMelee melee)
            {
                var list = Units.Where(x => x is BaseMelee).ToList();
                var nearestEnemy = melee.FindNearestEnemy();
                if (nearestEnemy != null) melee.Attack(nearestEnemy);
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
            unit.selected += () =>
            {
                SelectedUnit = unit;
                Instance._UIManager.ChangingUnitParam(unit);
            };
            Units.AddLast(unit);
        }

        public static IReadOnlyList<BaseUnit> GetUnits() => Units.ToList();
        public static StatsData GetStats() => Instance._statsConfig.AllProperties;
    }
}