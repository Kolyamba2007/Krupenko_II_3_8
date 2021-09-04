using UnityEngine.AI;

namespace Ziggurat.Units
{
    public class MeleeBehaviour : BaseUnitBehaviour
    {
        public MeleeBehaviour(BaseMelee unit, NavMeshAgent agent) : base(unit)
        {
            AddState<UnitMoveState>();
            AddState<UnitSeekState>();
            AddState<UnitAttackState>();
            AddState<UnitWanderState>();
        }
    }
}