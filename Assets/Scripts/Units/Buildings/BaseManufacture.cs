using System;
using System.Collections;
using UnityEngine;
using Ziggurat.UI;

namespace Ziggurat.Units
{
    public abstract class BaseManufacture : Building, IManufacture
    {
        [Header("Производство")]
        [SerializeField, Tooltip("Точка создания юнита")]
        private Transform _spawnPoint;
        [SerializeField, Min(0), Tooltip("Время производства (в сек.)")]
        private float _productionTime;
        [SerializeField]
        private ProgressBar ProgressBar;  

        public bool IsManufacturing { private set; get; } = false;
        public float ProductionProgress => ProgressBar.Value;
        public float ProductionTime { private set => _productionTime = value; get => _productionTime; }
        public Transform SpawnPoint => _spawnPoint;
        public Vector3? PoolPoint { set; get; }

        public event Action<Type> Manufactured;

        protected override void Disable()
        {
            base.Disable();
            if (IsManufacturing) Abort();
        }

        public BaseMelee ProduceUnit<T>() where T : BaseMelee
        {
            if (!IsManufacturing && ProductionTime > 0)
            {
                IsManufacturing = true;
                ProgressBar.SetValue(0);
                ProgressBar.Label = typeof(T).Name;
                StartCoroutine(ProduceCoroutine<T>());
            }
            return null;
        }
        private IEnumerator ProduceCoroutine<T>() where T : BaseMelee
        {
            float time = 0;
            while (time < ProductionTime)
            {
                if (Paused) continue;

                time += Time.deltaTime;
                ProgressBar.SetValue(time / ProductionTime);
                yield return null;
            }
            ProgressBar.SetValue(0);
            IsManufacturing = false;
            Manufactured?.Invoke(typeof(T));
        }
        public void Abort()
        {
            if (!IsManufacturing) return;
            IsManufacturing = false;
            ProgressBar.SetValue(0);
        }
    }
}