using UnityEngine;
using Ziggurat.UI;

namespace Ziggurat.Units
{
    public abstract class BaseManufacture : Building, IManufacture
    {
        [Header("Производство")]
        [SerializeField]
        protected ProgressBar ProgressBar;

        public bool IsManufacturing { private set; get; }
        public float ProductionProgress => ProgressBar.Value;

        public IUnit ProduceUnit<T>() where T : IUnit
        {
            throw new System.NotImplementedException();
        }
        public void Abort()
        {
            throw new System.NotImplementedException();
        }
    }
}