using UnityEngine;
using UnityEngine.AI;

namespace Ziggurat.Units
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseMelee : BaseUnit, IMovable
    {
        public override UnitType UnitType => UnitType.Melee;

        public bool CanMove { protected set; get; } = true;

        public Vector3 Velocity { protected set; get; }

        private Animator Animator { set; get; }
        private Rigidbody Rigidbody { set; get; }
        private NavMeshAgent NavMeshAgent { set; get; }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {      
            if (Target.HasValue)
            {
                NavMeshAgent.SetDestination(Target.Value);
            }
        }

        public bool MoveTo(Vector3 point)
        {
            if (!CanMove || Dead) return false;

            Target = new Vector3(point.x, Position.y, point.z);
            NavMeshAgent.destination = point;
            UnitState.MoveTo(this);
            return true;
        }
        public bool MoveTo(Transform target) => MoveTo(target.position);
        public bool MoveTo(IUnit target) => MoveTo(target.Position);
        public bool Seek(IUnit unit)
        {
            if (!CanMove || Dead) return false;

            Target = unit.Position;
            NavMeshAgent.destination = Target.Value;
            UnitState.Seek(this);
            return true;
        }
        public bool Wander(float radius)
        {
            if (!CanMove || Dead) return false;

            Target = Vector3.zero;
            NavMeshAgent.destination = Target.Value;
            UnitState.Wander(this);
            return true;
        }
    }
}