using UnityEngine.AI;

namespace Ziggurat.Units
{
    public class MeleeBehaviour : BaseUnitBehaviour
    {
        private readonly NavMeshAgent NavMeshAgent;
        public MeleeBehaviour(BaseUnit unit, NavMeshAgent agent) : base(unit) 
        {
            NavMeshAgent = agent;
            AddState<UnitMoveState>();
            AddState<UnitSeekState>();
            AddState<UnitWanderState>();
        }

        public void Move()
        {
            SwitchState<UnitMoveState>();
        }
        public void Seek()
        {
            SwitchState<UnitSeekState>();
        }
        public void Wander()
        {
            SwitchState<UnitWanderState>();
        }

        public void Update()
        {
            if (!Unit.Target.HasValue) return;

            float remainingDistance = (Unit.Target.Value.Position - Unit.Position).sqrMagnitude;
            float stoppingDistance = NavMeshAgent.stoppingDistance * NavMeshAgent.stoppingDistance;
            if (remainingDistance <= stoppingDistance)
            {
                if (CurrentState is UnitWanderState == false) Idle();
                return;
            }
            NavMeshAgent.SetDestination(Unit.Target.Value.Position);
        }
    }
}