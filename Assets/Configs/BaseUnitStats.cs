using System;
using UnityEngine;
using Ziggurat.Units;

namespace Ziggurat.Configuration
{
    [Serializable]
    public abstract class BaseUnitStats : ScriptableObject
    {
        public abstract StatsData AllProperties { get; }

        public abstract void SetStats(StatsData data);
    }
}