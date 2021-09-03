using UnityEngine;
using UnityEngine.AI;
using Ziggurat.Managers;

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

        private BattleParamsData BattleParams;

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.speed = GameManager.GetStats().MobilityParams.MoveSpeed;
            NavMeshAgent.angularSpeed = GameManager.GetStats().MobilityParams.RotateSpeed;

            MaxHealth = GameManager.GetStats().BaseParams.MaxHealth;
            Health = MaxHealth;

            BattleParams = GameManager.GetStats().BattleParams;

            BehaviourComponent = new MeleeBehaviour(this, NavMeshAgent);
        }
        protected override void Update()
        {
            base.Update();

            if (Behaviour == UnitState.Idle) return;
            (BehaviourComponent as MeleeBehaviour).Update();

            if (Behaviour == UnitState.Wander && NavMeshAgent.isStopped)
            {
                Target = new TargetPoint(Position + Random.insideUnitSphere * 5f);
            }
        }
        protected override void Disable()
        {
            base.Disable();
            CanMove = false;
            NavMeshAgent.isStopped = true;
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
            Behaviour = UnitState.Move;
            (BehaviourComponent as MeleeBehaviour).Move();
            return true;
        }
        public virtual bool MoveTo(Transform target)
        {
            if (!CanMove || Dead) return false;

            Target = new TargetPoint(target);
            NavMeshAgent.isStopped = false;
            Animator.SetFloat("Movement", 1f);
            Behaviour = UnitState.Move;
            (BehaviourComponent as MeleeBehaviour).Seek();
            return true;
        }
        public virtual bool MoveTo(BaseUnit target)
        {
            if (!CanMove || Dead || target.Dead) return false;

            return MoveTo(target.transform);
        }
        public virtual bool Wander(float radius)
        {
            if (!CanMove || Dead) return false;

            Target = new TargetPoint();
            NavMeshAgent.destination = Target.Value.Position;
            Behaviour = UnitState.Wander;
            (BehaviourComponent as MeleeBehaviour).Wander();
            return true;
        }
    }
}