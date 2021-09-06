using UnityEngine;
using UnityEngine.AI;

namespace Ziggurat.Units
{
    abstract class BaseMeleeState : BaseState
    {
        protected readonly new BaseMelee Unit;
        protected readonly NavMeshAgent NavMeshAgent;

        public BaseMeleeState(BaseMelee unit, MeleeBehaviour stateSwitcher) : base(unit, stateSwitcher)
        {
            Unit = unit;
            NavMeshAgent = stateSwitcher.NavMeshAgent;
        }
    }

    class UnitMoveState : BaseMeleeState
    {
        public UnitMoveState(BaseMelee unit, MeleeBehaviour stateSwitcher) : base(unit, stateSwitcher) { }

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
            if (Unit.ReachedTarget()) StateSwitcher.SwitchState<UnitIdleState>();
            else
            {
                NavMeshAgent.SetDestination(Unit.Target.Value.Position);
            }
        }
    }
    class UnitSeekState : BaseMeleeState
    {
        public UnitSeekState(BaseMelee unit, MeleeBehaviour stateSwitcher) : base(unit, stateSwitcher) { }

        public override void Start()
        {
            Unit.MoveTo(Unit.Target.Value.Target);
        }
        public override void Stop()
        {
            Unit.Animator.SetFloat("Movement", 0f);
        }
        public override void LogicUpdate()
        {
            if (Unit.ReachedTarget())
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
        public UnitAttackState(BaseMelee unit, MeleeBehaviour stateSwitcher) : base(unit, stateSwitcher) { }

        public override void Start()
        {
            Unit.transform.LookAt(Unit.Target.Value.Position);
        }
        public override void Stop()
        {
            Unit.Animator.SetBool("FastAttack", false);
            Unit.Animator.SetBool("StrongAttack", false);
        }
        public override void LogicUpdate()
        {
            if (Unit.ReachedTarget())
            {
                if (Unit.CanAttack) Unit.SetAttackAnimation(Random.value);
            }
            else
            {
                StateSwitcher.SwitchState<UnitSeekState>();
            }
        }
    }
    class UnitWanderState : BaseMeleeState
    {
        public UnitWanderState(BaseMelee unit, MeleeBehaviour stateSwitcher) : base(unit, stateSwitcher) { }

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