using UnityEngine;
using UnityEngine.AI;

namespace Ziggurat.Units
{
    abstract class BaseMeleeState : BaseState
    {
        protected readonly new BaseMelee Unit;
        protected readonly NavMeshAgent NavMeshAgent;
        public BaseMeleeState(BaseMelee unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher)
        {
            Unit = unit;
            NavMeshAgent = unit.NavMeshAgent;
        }

        protected bool ArrivedToTarget()
        {
            if (!Unit.Target.HasValue) return false;

            Vector3 targetPos = Unit.Target.Value.Position;
            targetPos.y = Unit.Position.y;
            float remainingDistance = (targetPos - Unit.Position).sqrMagnitude;
            float stoppingDistance = NavMeshAgent.stoppingDistance * NavMeshAgent.stoppingDistance;
            return remainingDistance <= stoppingDistance;
        }
    }

    class UnitMoveState : BaseMeleeState
    {
        public UnitMoveState(BaseMelee unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }

        public override void Start()
        {
            Unit.MoveTo(Unit.Target.Value.Position);
        }
        public override void Stop()
        {
            Unit.Animator.SetFloat("Movement", 0f);
        }
        public override void LogicUpdate()
        {
            if (ArrivedToTarget()) StateSwitcher.SwitchState<UnitIdleState>();
            else
            {
                NavMeshAgent.SetDestination(Unit.Target.Value.Position);
            }
        }
    }
    class UnitSeekState : BaseMeleeState
    {
        public UnitSeekState(BaseMelee unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }

        public override void Start()
        {
            Unit.MoveTo(Unit.Target.Value.Target);
        }
        public override void Stop()
        {
        }
        public override void LogicUpdate()
        {
            if (ArrivedToTarget())
            {
                if (Unit.IsAllied(Unit.Target.Value.Target)) StateSwitcher.SwitchState<UnitIdleState>();
                else StateSwitcher.SwitchState<UnitAttackState>();
            }
            else
            {
                NavMeshAgent.SetDestination(Unit.Target.Value.Target.Position);
            }
        }
    }
    class UnitAttackState : BaseMeleeState
    {
        public UnitAttackState(BaseMelee unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }

        public override void Start()
        {
            Unit.Attack(Unit.Target.Value.Target);
        }
        public override void Stop()
        {

        }
        public override void LogicUpdate()
        {
            if (ArrivedToTarget() && Unit.CanAttack)
            {
                Unit.Attack(Unit.Target.Value.Target);
            }
            else
            {
                StateSwitcher.SwitchState<UnitSeekState>();
            }
        }
    }
    class UnitWanderState : BaseMeleeState
    {
        public UnitWanderState(BaseMelee unit, IStateSwitcher stateSwitcher) : base(unit, stateSwitcher) { }

        public override void Start()
        {

        }
        public override void Stop()
        {

        }
        public override void LogicUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}