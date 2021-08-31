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

        public BaseMelee ProduceUnit<T>() where T : BaseMelee
        {
            IsManufacturing = true;
            ProgressBar.SetValue(0);
            return null;
        }
        public void Abort()
        {
            IsManufacturing = false;
            ProgressBar.SetValue(0);
        }
    }
}