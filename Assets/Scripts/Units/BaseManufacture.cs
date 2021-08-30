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

        public IUnit ProduceUnit<T>() where T : IUnit
        {
            IsManufacturing = true;
            ProgressBar.ProductName = typeof(T).Name;
            return null;
        }
        public void Abort()
        {
            IsManufacturing = false;
            ProgressBar.SetValue(0);
        }
    }
}