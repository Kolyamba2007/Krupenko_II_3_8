namespace Ziggurat.Units
{
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
        public abstract void LogicUpdate();
    }

    class UnitIdleState : BaseState
    {
        public UnitIdleState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }

        public override void Start()
        {
            Unit.Idle();
        }
        public override void Stop()
        {

        }
        public override void LogicUpdate()
        {
            
        }
    }
    class UnitDeadState : BaseState
    {
        public UnitDeadState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
            
        public override void Start()
        {
            Unit.Die();
        }
        public override void Stop()
        {

        }
        public override void LogicUpdate()
        {
       
        }
    }
}