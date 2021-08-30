using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat.Units
{
    public abstract class Building : BaseUnit
    {
        public override UnitType UnitType => UnitType.Building;
    }
}