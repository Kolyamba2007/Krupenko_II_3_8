using System;
using System.Collections;
using UnityEngine;
using Ziggurat.UI;

namespace Ziggurat.Units
{
    public abstract class BaseManufacture : Building, IManufacture
    {
        [Header("Производство")]
        [SerializeField, Min(0), Tooltip("Время производства")]
        private float _productionTime;
        [SerializeField]
        protected ProgressBar ProgressBar;

        public bool IsManufacturing { private set; get; } = false;
        public float ProductionProgress => ProgressBar.Value;
        public float ProductionTime { private set => _productionTime = value; get => _productionTime; }

        public event Action<BaseMelee> Manufactured;

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
            Manufactured?.Invoke(null);
        }
        public void Abort()
        {
            if (!IsManufacturing) return;
            IsManufacturing = false;
            ProgressBar.SetValue(0);
        }
    }
}