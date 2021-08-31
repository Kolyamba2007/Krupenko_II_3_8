namespace Ziggurat.Units
{
    public interface IUnitState
    {
        void Idle(BaseUnit unit);
        void MoveTo(BaseUnit unit);
        void Seek(BaseUnit unit);
        void Wander(BaseUnit unit); 
    }

    class UnitIdleState : IUnitState
    {
        public void Idle(BaseUnit unit)
        {
            return;
        }

        public void MoveTo(BaseUnit unit)
        {
            unit.SetState(new UnitMoveState());
        }

        public void Seek(BaseUnit unit)
        {
            unit.SetState(new UnitSeekState());
        }

        public void Wander(BaseUnit unit)
        {
            unit.SetState(new UnitWanderState());
        }
    }
    class UnitMoveState : IUnitState
    {
        public void Idle(BaseUnit unit)
        {
            unit.SetState(new UnitIdleState());
        }

        public void MoveTo(BaseUnit unit)
        {
            return;
        }

        public void Seek(BaseUnit unit)
        {
            unit.SetState(new UnitSeekState());
        }

        public void Wander(BaseUnit unit)
        {
            unit.SetState(new UnitWanderState());
        }
    }
    class UnitSeekState : IUnitState
    {
        public void Idle(BaseUnit unit)
        {
            unit.SetState(new UnitIdleState());
        }

        public void MoveTo(BaseUnit unit)
        {
            unit.SetState(new UnitMoveState());
        }

        public void Seek(BaseUnit unit)
        {
            return;
        }

        public void Wander(BaseUnit unit)
        {
            unit.SetState(new UnitWanderState());
        }
    }
    class UnitWanderState : IUnitState
    {
        public void Idle(BaseUnit unit)
        {
            unit.SetState(new UnitIdleState());
        }

        public void MoveTo(BaseUnit unit)
        {
            unit.SetState(new UnitMoveState());
        }

        public void Seek(BaseUnit unit)
        {
            unit.SetState(new UnitSeekState());
        }

        public void Wander(BaseUnit unit)
        {
            return;
        }
    }
}