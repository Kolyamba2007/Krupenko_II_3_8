using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Ziggurat.Managers;

namespace Ziggurat.Units
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseMelee : BaseUnit, IMovable, IAttackable
    {
        private Rigidbody Rigidbody { set; get; }       

        public override UnitType UnitType => UnitType.Melee;

        public bool CanAttack { private set; get; } = true;
        public bool CanMove { private set; get; } = true;

        public float MovementSpeed { private set; get; }
        public float AttackCooldown { private set; get; }
        public NavMeshAgent NavMeshAgent { private set; get; }

        protected new MeleeBehaviour BehaviourComponent { private set; get; }

        protected override void Awake()
        {
            base.Awake();

            StatsData data = GameManager.GetStats();
            MaxHealth = data.BaseParams.MaxHealth;
            Health = MaxHealth;
            MovementSpeed = data.MobilityParams.MoveSpeed;
            AttackCooldown = data.BattleParams.AttackCooldown;

            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.speed = MovementSpeed;
            NavMeshAgent.angularSpeed = data.MobilityParams.RotateSpeed;           

            BehaviourComponent = new MeleeBehaviour(this, NavMeshAgent);
        }
        protected override void Update()
        {
            base.Update();
            BehaviourComponent.CurrentState.LogicUpdate();
        }
        protected override void Disable()
        {
            base.Disable();
            CanMove = false;
            CanAttack = false;
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
            BehaviourComponent.SwitchState<UnitMoveState>();
            return true;
        }
        public virtual bool MoveTo(Transform target)
        {
            if (!CanMove || Dead) return false;

            Target = new TargetPoint(target);
            NavMeshAgent.isStopped = false;
            Animator.SetFloat("Movement", 1f);
            Behaviour = UnitState.Move;
            BehaviourComponent.SwitchState<UnitSeekState>();
            return true;
        }
        public virtual bool MoveTo(BaseUnit target)
        {
            if (!CanMove || Dead || target.Dead) return false;

            Target = new TargetPoint(target);
            NavMeshAgent.isStopped = false;
            Animator.SetFloat("Movement", 1f);
            Behaviour = UnitState.Move;
            BehaviourComponent.SwitchState<UnitSeekState>();
            return true;
        }
        public virtual bool Attack(BaseUnit target)
        {
            if (Dead || target.Dead || target.Invulnerable) return false;

            Behaviour = UnitState.Attack;
            if (CanAttack)
            {
                Animator.SetTrigger("FastAttack");
                StartCoroutine(AttackCoroutine());
            }
            BehaviourComponent.SwitchState<UnitAttackState>();
            return true;
        }
        private IEnumerator AttackCoroutine()
        {
            CanAttack = false;
            yield return new WaitForSeconds(AttackCooldown);
            CanAttack = true;
        }
        public virtual bool Wander(float radius)
        {
            if (!CanMove || Dead) return false;

            Target = new TargetPoint();
            NavMeshAgent.destination = Target.Value.Position;
            Behaviour = UnitState.Wander;
            BehaviourComponent.SwitchState<UnitWanderState>();
            return true;
        }
    }
}