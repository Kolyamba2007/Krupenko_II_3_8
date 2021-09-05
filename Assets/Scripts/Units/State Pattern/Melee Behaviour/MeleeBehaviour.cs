using UnityEngine.AI;

namespace Ziggurat.Units
{
    public class MeleeBehaviour : BaseUnitBehaviour, IStateSwitcher
    {
        public readonly NavMeshAgent NavMeshAgent;
        public MeleeBehaviour(BaseMelee unit, NavMeshAgent agent) : base(unit)
        {
            NavMeshAgent = agent;
            AddState<UnitMoveState>();
            AddState<UnitSeekState>();
            AddState<UnitAttackState>();
            AddState<UnitWanderState>();
        }
    }
}