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

            BehaviourComponent = new MeleeBehaviour(this);
        }
        protected virtual void Update()
        {
            if (CurrentState is UnitIdleState) return;

            var component = BehaviourComponent as MeleeBehaviour;
            switch (Behaviour)
            {
                case UnitState.Move:
                    component.Move(NavMeshAgent);
                    break;
                case UnitState.Seek:
                    component.Seek(NavMeshAgent);
                    break;                    
            }           
        }
        protected override void Disable()
        {
            base.Disable();
            CanMove = false;
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

            Target = new TargetPoint(new Vector3(point.x, Position.y, point.z));
            NavMeshAgent.destination = point;
            NavMeshAgent.isStopped = false;
            Animator.SetFloat("Movement", 1f);
            BehaviourComponent.SwitchState<UnitMoveState>();
            return true;
        }
        public virtual bool MoveTo(Transform target)
        {
            if (!CanMove || Dead) return false;

            Target = new TargetPoint(target);
            NavMeshAgent.isStopped = false;
            Animator.SetFloat("Movement", 1f);
            BehaviourComponent.SwitchState<UnitSeekState>();
            return true;
        }
        public virtual bool MoveTo(BaseUnit target)
        {
            if (!CanMove || Dead || target.Dead) return false;

            return MoveTo(target.transform);
        }
        public virtual bool Seek(BaseUnit unit)
        {
            if (!CanMove || Dead) return false;

            Target = new TargetPoint(unit);
            NavMeshAgent.destination = Target.Value.Position;
            BehaviourComponent.SwitchState<UnitSeekState>();
            return true;
        }
        public virtual bool Wander(float radius)
        {
            if (!CanMove || Dead) return false;

            Target = new TargetPoint();
            NavMeshAgent.destination = Target.Value.Position;
            BehaviourComponent.SwitchState<UnitWanderState>();
            return true;
        }
    }
}