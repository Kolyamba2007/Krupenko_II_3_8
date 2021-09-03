﻿using System;
using UnityEngine;
using Ziggurat.UI;

namespace Ziggurat.Units
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ClickComponent))]
    public abstract class BaseUnit : MonoBehaviour, ISelectable
    {
        public abstract string Name { get; }
        public Vector3 Position => transform.position;
        public abstract UnitType UnitType { get; }
        public TargetPoint? Target { protected set; get; }

        #region Statuses
        [field: Header("Статусы")]
        [field: SerializeField, RenameField("Paused"), Tooltip("Неактивный")]
        public bool Paused { private set; get; }
        [field: SerializeField, RenameField("Invulnerable"), Tooltip("Неуязвим")]
        public bool Invulnerable { private set; get; }
        [field: SerializeField, RenameField("Can Regenerate"), Tooltip("Может восстанавливаться")]
        public bool CanRegenerate { private set; get; }
        public bool Selectable { private set => ClickComponent.Selectable = value; get => ClickComponent.Selectable; }
        public bool Selected => ClickComponent.Selected;
        public bool Dead => Health == 0;
        #endregion

        #region Characteristics
        private ClickComponent ClickComponent { set; get; }  

        [field: Header("Характеристики")]
        [field: SerializeField, RenameField("Health"), Tooltip("Здоровье юнита")]
        public ushort Health { protected set; get; }
        [field: SerializeField, RenameField("Max Health"), Tooltip("Максимальное здоровье юнита")]
        public ushort MaxHealth { protected set; get; }
        [field: SerializeField, RenameField("Owner"), Tooltip("Владелец юнита")]
        public Owner Owner { set; get; } = Owner.Neutral;

        private float _hpPerSec = 1f;
        #endregion

        #region Behaviour
        public UnitState Behaviour { protected set; get; }
        protected BaseState CurrentState => BehaviourComponent.CurrentState;
        protected IStateSwitcher BehaviourComponent { set; get; }
        #endregion

        #region Events
        public event Action died;
        public event Action selected;
        #endregion

        protected virtual void Awake()
        {
            BehaviourComponent = new BaseUnitBehaviour(this); 

            ClickComponent = GetComponent<ClickComponent>();
            ClickComponent.selected += selected;

            Managers.GameManager.RegisterUnit(this);
        }
        protected virtual void Disable()
        {
            Selectable = false;
            CanRegenerate = false;
            CurrentState.Stop();
            Target = null;            
            ClickComponent.Select(false);
        }
        protected virtual void Update()
        {
            if (!CanRegenerate || Health == MaxHealth) return;
            if (_hpPerSec > 0)
            {
                _hpPerSec -= Time.deltaTime;
            }
            else
            {
                SetHealth((ushort)(Health + 1));
                _hpPerSec = 1f;
            }
        }

        public bool SetDamage(byte value) => SetDamage((ushort)value);
        public bool SetDamage(ushort value)
        {
            if (Invulnerable) return false;

            if (Health - value > 0) Health -= value;
            else
            {
                Health = 0;
                Disable();
                Behaviour = UnitState.Die;
                BehaviourComponent.Die();
                died?.Invoke();
            }
            return true;
        }
        public bool IsAllied(BaseUnit unit) => Owner == unit.Owner;
        public void SetHealth(ushort value)
        {
            if (value <= MaxHealth) Health = value;
            else Health = MaxHealth;
        }
        public virtual void Idle()
        {
            Target = null;
            Behaviour = UnitState.Idle;
        }
    }
}