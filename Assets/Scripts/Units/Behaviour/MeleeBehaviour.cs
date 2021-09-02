using UnityEngine.AI;

namespace Ziggurat.Units
{
    public class MeleeBehaviour : BaseUnitBehaviour
    {
        public MeleeBehaviour(BaseUnit unit) : base(unit) 
        {
            AddState<UnitMoveState>();
            AddState<UnitSeekState>();
            AddState<UnitWanderState>();
        }

        public void Move(NavMeshAgent agent)
        {
            if (!Unit.Target.HasValue) return;

            float remainingDistance = (agent.destination - Unit.Position).sqrMagnitude;
            float stoppingDistance = agent.stoppingDistance * agent.stoppingDistance;
            if (remainingDistance <= stoppingDistance)
            {
                Idle();
                return;
            }
            agent.SetDestination(Unit.Target.Value.Position);
        }
        public void Seek(NavMeshAgent agent)
        {
            if (!Unit.Target.HasValue) return;

            float remainingDistance = (Unit.Target.Value.Position - Unit.Position).sqrMagnitude;
            float stoppingDistance = agent.stoppingDistance * agent.stoppingDistance;
            if (remainingDistance <= stoppingDistance)
            {
                Idle();
                return;
            }
            agent.SetDestination(Unit.Target.Value.Position);
        }
        public void Wander(NavMeshAgent agent)
        {

        }
    }
}