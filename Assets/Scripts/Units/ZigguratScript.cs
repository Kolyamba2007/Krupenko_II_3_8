using UnityEngine;
using Ziggurat.UI;

namespace Ziggurat.Units
{
    public class ZigguratScript : Building, IManufacture
    {
        [Header("Производство")]
        [SerializeField]
        private ProgressBar ProgressBar;

        public bool IsManufacturing { private set; get; }
        public float ProductionProgress => ProgressBar.Value;

        public IUnit Produce<T>() where T : IUnit
        {
            throw new System.NotImplementedException();
        }

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