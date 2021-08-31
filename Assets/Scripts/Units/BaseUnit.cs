﻿using System;
using UnityEngine;

namespace Ziggurat.Units
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider))]
    public abstract class BaseUnit : MonoBehaviour, IUnit
    {
        public abstract string Name { get; }
        public Vector3 Position { protected set; get; }
        public abstract UnitType UnitType { get; }
        public Vector3 Target { protected set; get; }

        #region Statuses
        [field: Header("Статусы")]
        [field: SerializeField, RenameField("Paused"), Tooltip("Неактивный")]
        public bool Paused { private set; get; }
        [field: SerializeField, RenameField("Invincible"), Tooltip("Неуязвим")]
        public bool Invincible { private set; get; }
        [field: SerializeField, RenameField("Selectable"), Tooltip("Выделяем")]
        public bool Selectable { private set; get; }
        public bool Selected { private set; get; }
        #endregion

        #region Characteristics
        [field: Header("Характеристики")]
        [field: SerializeField, RenameField("Health"), Tooltip("Здоровье юнита")]
        public ushort Health { private set; get; }
        [field: SerializeField, RenameField("Owner"), Tooltip("Владелец юнита")]
        public Owner Owner { protected set; get; }
        #endregion

        #region Events
        public event Action died;
        public event Action selected;
        #endregion

        protected Collider Collider { set; get; }  

        private void Awake()
        {
            Collider = GetComponent<Collider>();
        }

        public bool SetDamage(byte value) => SetDamage((ushort)value);
        public bool SetDamage(ushort value)
        {
            if (Invincible) return false;

            if (Health - value > 0) Health -= value;
            else
            {
                Health = 0;
                died?.Invoke();
            }
            return true;
        }
        public bool IsAllied(IUnit unit) => Owner == unit.Owner;
    }
}