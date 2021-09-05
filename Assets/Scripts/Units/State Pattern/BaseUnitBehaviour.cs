using System;
using System.Linq;
using System.Collections.Generic;

namespace Ziggurat.Units
{
    public abstract class BaseUnitBehaviour : IStateSwitcher
    {
        private readonly BaseUnit Unit;
        private List<BaseState> States { set; get; } = new List<BaseState>();
        public BaseState CurrentState { private set; get; }

        public bool IsActive { private set; get; }

        public BaseUnitBehaviour(BaseUnit unit)
        {
            Unit = unit;
            AddState<UnitIdleState>();
            AddState<UnitDeadState>();
            Start();
        }

        public virtual void Start()
        {
            SwitchState<UnitIdleState>();
            IsActive = true;
        }
        public virtual void Stop()
        {
            CurrentState.Stop();
            IsActive = false;
        }

        protected void AddState<T>() where T : BaseState
        {
            States.Add((BaseState)Activator.CreateInstance(typeof(T), Unit, this));
        }
        public void SwitchState<T>() where T : BaseState
        {
            if (CurrentState is T) return;

            CurrentState?.Stop();
            CurrentState = States.FirstOrDefault(state => state is T);
            CurrentState?.Start();
        }       
    }
}