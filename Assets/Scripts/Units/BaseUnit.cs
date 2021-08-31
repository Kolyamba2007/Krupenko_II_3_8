using System;
using UnityEngine;

namespace Ziggurat.Units
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider))]
    public abstract class BaseUnit : MonoBehaviour, IUnit
    {
        public abstract string Name { get; }
        public Vector3 Position => transform.position;
        public abstract UnitType UnitType { get; }
        public Vector3? Target { protected set; get; }

        #region Statuses
        [field: Header("Статусы")]
        [field: SerializeField, RenameField("Paused"), Tooltip("Неактивный")]
        public bool Paused { private set; get; }
        [field: SerializeField, RenameField("Invincible"), Tooltip("Неуязвим")]
        public bool Invulnerable { private set; get; }
        [field: SerializeField, RenameField("Selectable"), Tooltip("Выделяем")]
        public bool Selectable { private set; get; }
        public bool Selected { private set; get; }
        public bool Dead => Health == 0;
        #endregion

        #region Characteristics
        public IUnitState UnitState { set; get; } = new UnitIdleState();

        [field: Header("Характеристики")]
        [field: SerializeField, RenameField("Health"), Tooltip("Здоровье юнита")]
        public ushort Health { private set; get; }
        [field: SerializeField, RenameField("Max Health"), Tooltip("Максимальное здоровье юнита")]
        public ushort MaxHealth { private set; get; }
        [field: SerializeField, RenameField("Owner"), Tooltip("Владелец юнита")]
        public Owner Owner { private set; get; }
        public UnitState CurrentState
        {
            get
            {
                if (UnitState is UnitIdleState) return Ziggurat.UnitState.Idle;
                if (UnitState is UnitMoveState) return Ziggurat.UnitState.Move;
                if (UnitState is UnitSeekState) return Ziggurat.UnitState.Seek;
                if (UnitState is UnitWanderState) return Ziggurat.UnitState.Wander;
                throw new Exception();
            }
        }
        #endregion

        #region Events
        public event Action died;
        public event Action selected;
        #endregion

        protected Collider Collider { set; get; }  

        private void Awake()
        {
            Collider = GetComponent<Collider>();

            Health = MaxHealth;
        }

        public bool SetDamage(byte value) => SetDamage((ushort)value);
        public bool SetDamage(ushort value)
        {
            if (Invulnerable) return false;

            if (Health - value > 0) Health -= value;
            else
            {
                Health = 0;
                died?.Invoke();
            }
            return true;
        }
        public bool IsAllied(IUnit unit) => Owner == unit.Owner;
        public void Idle()
        {
            UnitState.Idle(this);
            Target = null;
        }
    }
}