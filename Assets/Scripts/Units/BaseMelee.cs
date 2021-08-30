using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat.Units
{
    public abstract class BaseMelee : BaseUnit
    {
        public override UnitType UnitType => UnitType.Melee;
    }
}