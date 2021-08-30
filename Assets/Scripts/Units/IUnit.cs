using System;
using UnityEngine;

namespace Ziggurat.Units
{
    public interface IUnit
    {
        string Name { get; }
        Vector3 Position { get; }
        UnitType UnitType { get; }
        Vector3 Target { get; }

        #region Statuses
        /// <summary>
        /// Неактивный
        /// </summary>
        bool Paused { get; }
        /// <summary>
        /// Неуязвимый
        /// </summary>
        bool Invincible { get; }
        /// <summary>
        /// Выделяем
        /// </summary>
        bool Selectable { get; }
        #endregion

        #region Characteristics
        /// <summary>
        /// Здоровье юнита
        /// </summary>
        ushort Health { get; }
        #endregion
        
        #region Events
        event Action died;
        event Action selected;
        #endregion

        #region Methods
        bool SetDamage(byte value);
        bool SetDamage(ushort value);
        #endregion
    }
}
