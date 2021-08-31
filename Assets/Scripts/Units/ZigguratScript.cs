using UnityEngine;
using Ziggurat.UI;

namespace Ziggurat.Units
{
    public class ZigguratScript : BaseManufacture
    {
        public override string Name => "Ziggurat";

        private void Start()
        {
            ProduceUnit<KnightScript>();
        }
    }
}