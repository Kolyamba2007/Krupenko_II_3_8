namespace Ziggurat.Units
{
    public interface IStateSwitcher
    {
        BaseState CurrentState { get; }
        void SwitchState<T>() where T : BaseState;
        void Idle();
        void Die();
    }

    public abstract class BaseState
    {
        protected readonly BaseUnit Unit;
        protected readonly IStateSwitcher StateSwitcher;

        public BaseState(BaseUnit unit, IStateSwitcher stateSwitcher)
        {
            Unit = unit;
            StateSwitcher = stateSwitcher;
        }
        public abstract void Start();
        public abstract void Stop();
        public abstract void Idle();
        public abstract void Die();
    }

    class UnitIdleState : BaseState
    {
        public UnitIdleState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
          
        public override void Start()
        {
            Unit.Behaviour = UnitState.Idle;
        }
        public override void Stop()
        {
            
        }
        public override void Idle()
        {
            return;
        }
        public override void Die()
        {
            StateSwitcher.SwitchState<UnitDeadState>();
        }
    }
    class UnitDeadState : BaseState
    {
        public UnitDeadState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
            
        public override void Start()
        {
            Unit.Behaviour = UnitState.Die;
        }
        public override void Stop()
        {

        }
        public override void Idle()
        {
            return;
        }
        public override void Die()
        {
            return;
        }
    }
    class UnitMoveState : BaseState
    {
        public UnitMoveState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { } 
 
        public override void Start()
        {
            Unit.Behaviour = UnitState.Move;
        }
        public override void Stop()
        {
            
        }
        public override void Idle()
        {
            StateSwitcher.SwitchState<UnitIdleState>();
        }
        public override void Die()
        {
            StateSwitcher.SwitchState<UnitDeadState>();
        }

    }
    class UnitSeekState : BaseState
    {
        public UnitSeekState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
           
        public override void Start()
        {
            Unit.Behaviour = UnitState.Seek;
        }
        public override void Stop()
        {
           
        }
        public override void Idle()
        {
            StateSwitcher.SwitchState<UnitIdleState>();
        }
        public override void Die()
        {
            StateSwitcher.SwitchState<UnitDeadState>();
        }
    }
    class UnitWanderState : BaseState
    {
        public UnitWanderState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
            
        public override void Start()
        {
            Unit.Behaviour = UnitState.Wander;
        }
        public override void Stop()
        {
         
        }
        public override void Idle()
        {
            StateSwitcher.SwitchState<UnitIdleState>();
        }
        public override void Die()
        {
            StateSwitcher.SwitchState<UnitDeadState>();
        }
    }
}