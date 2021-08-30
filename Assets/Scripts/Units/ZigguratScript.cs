using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat.Units
{
    public class ZigguratScript : Building, IManufacture
    {
        public IUnit Produce<T>() where T : IUnit
        {
            throw new System.NotImplementedException();
        }
    }
}