using UnityEngine;

namespace Ziggurat.Units
{
    public interface IUnit
    {
        string Name { get; }
        Vector3 Position { get; }
        UnitType UnitType { get; }

        #region Statuses
        bool Paused { get; }
        bool Invincible { get; }
        #endregion

        #region Characteristics
        byte Health { get; }
        #endregion

        Vector3 Target { get; }    
    }
}
