using System;
using UnityEngine;

namespace Ziggurat.Units
{
    [DisallowMultipleComponent]
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
        [field: SerializeField, RenameField("Invulnerable"), Tooltip("Неуязвим")]
        public bool Invulnerable { private set; get; }
        [field: SerializeField, RenameField("Selectable"), Tooltip("Выделяем")]
        public bool Selectable { private set; get; }
        public bool Selected { private set; get; }
        public bool Dead => Health == 0;
        #endregion

        #region Characteristics
        protected IUnitState UnitState { private set; get; } = new UnitIdleState();

        [field: Header("Характеристики")]
        [field: SerializeField, RenameField("Health"), Tooltip("Здоровье юнита")]
        public ushort Health { private set; get; }
        [field: SerializeField, RenameField("Max Health"), Tooltip("Максимальное здоровье юнита")]
        public ushort MaxHealth { private set; get; }
        [field: SerializeField, RenameField("Owner"), Tooltip("Владелец юнита")]
        public Owner Owner { private set; get; } = Owner.Neutral;
        public UnitState CurrentState { private set; get; } = Ziggurat.UnitState.Idle;
        #endregion

        #region Events
        public event Action died;
        public event Action selected;
        #endregion

        private void Awake()
        {
            Managers.GameManager.RegisterUnit(this);
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
        public virtual void Idle()
        {
            UnitState.Idle(this);
            Target = null;
        }
        public void SetState(IUnitState state)
        {
            UnitState = state;
            if (UnitState is UnitIdleState) CurrentState = Ziggurat.UnitState.Idle;
            if (UnitState is UnitMoveState) CurrentState = Ziggurat.UnitState.Move;
            if (UnitState is UnitSeekState) CurrentState = Ziggurat.UnitState.Seek;
            if (UnitState is UnitWanderState) CurrentState = Ziggurat.UnitState.Wander;
        }
    }
}