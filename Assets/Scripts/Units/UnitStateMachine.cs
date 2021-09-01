namespace Ziggurat.Units
{
    public interface IStateSwitcher
    {
        void AddState<T>() where T : BaseState;
        void SwitchState<T>() where T : BaseState;
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
        public abstract void Idle(BaseUnit unit);
    }

    class UnitIdleState : BaseState
    {
        public UnitIdleState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
        public override void Idle(BaseUnit unit)
        {
            return;
        }
        public override void Start()
        {

        }
        public override void Stop()
        {
            
        }
    }
    class UnitMoveState : BaseState
    {
        public UnitMoveState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
        public override void Idle(BaseUnit unit)
        {
            StateSwitcher.SwitchState<UnitIdleState>();
        }
        public override void Start()
        {
            
        }
        public override void Stop()
        {
            
        }
    }
    class UnitSeekState : BaseState
    {
        public UnitSeekState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
        public override void Idle(BaseUnit unit)
        {
            StateSwitcher.SwitchState<UnitIdleState>();
        }
        public override void Start()
        {
          
        }
        public override void Stop()
        {
           
        }
    }
    class UnitWanderState : BaseState
    {
        public UnitWanderState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
        public override void Idle(BaseUnit unit)
        {
            StateSwitcher.SwitchState<UnitIdleState>();
        }
        public override void Start()
        {
            
        }
        public override void Stop()
        {
         
        }
    }
}