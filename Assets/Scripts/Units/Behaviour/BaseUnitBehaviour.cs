using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.AI;

namespace Ziggurat.Units
{
    public class BaseUnitBehaviour : IStateSwitcher
    {
        protected readonly BaseUnit Unit;
        private List<BaseState> States { set; get; } = new List<BaseState>();
        public BaseState CurrentState { private set; get; }
        
        public BaseUnitBehaviour(BaseUnit unit)
        {
            Unit = unit;
            AddState<UnitIdleState>();
            AddState<UnitDeadState>();
            SwitchState<UnitIdleState>();
        }

        public void Idle()
        {
            Unit.Idle();
            CurrentState.Idle();
        }
        public void Die()
        {
            CurrentState.Die();
        }      

        protected void AddState<T>() where T : BaseState
        {
            States.Add((BaseState)Activator.CreateInstance(typeof(T), Unit, this));
        }
        public void SwitchState<T>() where T : BaseState
        {
            CurrentState?.Stop();
            CurrentState = States.FirstOrDefault(state => state is T);
            CurrentState.Start();
        }       
    }
}