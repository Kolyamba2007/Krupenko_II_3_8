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
            Unit.Idle();
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
    class UnitDeadState : BaseState
    {
        public UnitDeadState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
            
        public override void Start()
        {
           
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

    interface IMeleeState
    {
        void Move();
        void Seek();
        void Wander();
        void Attack();
    }
    class UnitMoveState : BaseState, IMeleeState
    {
        public UnitMoveState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }

        public override void Start()
        {
            
        }
        public override void Stop()
        {
            
        }

        public void Move()
        {
            return;
        }
        public void Seek()
        {
            StateSwitcher.SwitchState<UnitSeekState>();
        }
        public void Wander()
        {
            StateSwitcher.SwitchState<UnitWanderState>();
        }
        public void Attack()
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
    class UnitSeekState : BaseState, IMeleeState
    {
        public UnitSeekState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }
        
        public override void Start()
        {
         
        }
        public override void Stop()
        {
           
        }

        public void Move()
        {
            StateSwitcher.SwitchState<UnitMoveState>();
        }
        public void Seek()
        {
            return;
        }
        public void Wander()
        {
            StateSwitcher.SwitchState<UnitWanderState>();
        }
        public void Attack()
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
    class UnitWanderState : BaseState, IMeleeState
    {
        public UnitWanderState(BaseUnit unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }

        public override void Start()
        {
           
        }
        public override void Stop()
        {
         
        }

        public void Move()
        {
            StateSwitcher.SwitchState<UnitMoveState>();
        }
        public void Seek()
        {
            StateSwitcher.SwitchState<UnitSeekState>();
        }
        public void Wander()
        {
            return;
        }
        public void Attack()
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