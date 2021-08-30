using System;
using UnityEngine;

namespace Ziggurat.Units
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider))]
    public abstract class BaseUnit : MonoBehaviour, IUnit
    {
        public string Name => transform.name;
        public Vector3 Position { protected set; get; }
        public abstract UnitType UnitType { get; }
        public Vector3 Target { protected set; get; }

        #region Statuses
        [Header("Статусы")]
        [SerializeField, Tooltip("Неактивный")]
        private bool _paused;
        [SerializeField, Tooltip("Неуязвим")]
        private bool _invincible;
        [SerializeField, Tooltip("Выделяем")]
        private bool _selectable;

        public bool Paused { protected set => _paused = value; get => _paused; }
        public bool Invincible { protected set => _invincible = value; get => _invincible; }
        public bool Selectable { protected set => _selectable = value; get => _selectable; }
        public bool Selected { protected set; get; }
        #endregion

        #region Characteristics
        [Header("Характеристики")]
        [SerializeField, Tooltip("Здоровье юнита")]
        private ushort _health;
        public ushort Health => _health;
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

            if (Health - value > 0) _health -= value;
            else
            {
                _health = 0;
                died?.Invoke();
            }
            return true;
        }
    }
}