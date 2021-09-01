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

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            NavMeshAgent = GetComponent<NavMeshAgent>();

            BehaviourComponent.AddState<UnitMoveState>();
            BehaviourComponent.AddState<UnitSeekState>();
            BehaviourComponent.AddState<UnitWanderState>();
        }
        private void Update()
        {
            if (!Target.HasValue) return;

            float remainingDistance = (NavMeshAgent.destination - Position).sqrMagnitude;
            float stoppingDistance = NavMeshAgent.stoppingDistance * NavMeshAgent.stoppingDistance;
            if (remainingDistance <= stoppingDistance)
            {
                Idle();
                return;
            }

            NavMeshAgent.SetDestination(Target.Value);            
        }

        public override void Idle()
        {
            base.Idle();
            NavMeshAgent.destination = Position;
            NavMeshAgent.isStopped = true;
            Animator.SetFloat("Movement", 0f);
        }
        public virtual bool MoveTo(Vector3 point)
        {
            if (!CanMove || Dead) return false;

            Target = new Vector3(point.x, Position.y, point.z);
            NavMeshAgent.destination = point;
            NavMeshAgent.isStopped = false;
            Animator.SetFloat("Movement", 1f);
            BehaviourComponent.SwitchState<UnitMoveState>();
            return true;
        }
        public virtual bool MoveTo(Transform target) => MoveTo(target.position);
        public virtual bool MoveTo(BaseUnit target) => MoveTo(target.Position);
        public virtual bool Seek(BaseUnit unit)
        {
            if (!CanMove || Dead) return false;

            Target = unit.Position;
            NavMeshAgent.destination = Target.Value;
            BehaviourComponent.SwitchState<UnitSeekState>();
            return true;
        }
        public virtual bool Wander(float radius)
        {
            if (!CanMove || Dead) return false;

            Target = Vector3.zero;
            NavMeshAgent.destination = Target.Value;
            BehaviourComponent.SwitchState<UnitWanderState>();
            return true;
        }
    }
}