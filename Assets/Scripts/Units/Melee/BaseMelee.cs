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
        private BattleParamsData BattleParams;
        private ProbabilityParamsData ProbabilityParams;
        private Rigidbody Rigidbody { set; get; }       
        private NavMeshAgent NavMeshAgent { set; get; }

        public override UnitType UnitType => UnitType.Melee;

        public bool CanAttack { private set; get; } = true;
        public bool CanMove { private set; get; } = true;

        public float MovementSpeed { private set; get; }
        public float AttackCooldown { private set; get; }

        public override void SetParams(StatsData data)
        {
            base.SetParams(data);
            ProbabilityParams = data.ProbabilityParams;
            BattleParams = data.BattleParams;
            MovementSpeed = data.MobilityParams.MoveSpeed;
            AttackCooldown = data.BattleParams.AttackCooldown;

            NavMeshAgent.speed = MovementSpeed;
            NavMeshAgent.angularSpeed = data.MobilityParams.RotateSpeed;     
        }

        protected override void Awake()
        {
            base.Awake();

            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            NavMeshAgent = GetComponent<NavMeshAgent>();                 

            BehaviourController = new MeleeBehaviour(this, NavMeshAgent);
        }
        protected override void Update()
        {
            base.Update();
            if (BehaviourController.IsActive) BehaviourController.CurrentState.LogicUpdate();
        }
        protected override void Disable()
        {
            base.Disable();
            CanMove = false;
            CanAttack = false;
            NavMeshAgent.isStopped = true;
            Rigidbody.isKinematic = true;
            Rigidbody.detectCollisions = false;
            NavMeshAgent.enabled = false;
        }

        private void OnTargetDied()
        {
            if (Target.HasValue)
            {
                Target.Value.Target.died -= OnTargetDied;
                Target = null;
            }

            var nearestEnemy = this.FindNearestEnemy();
            if (nearestEnemy != null) Attack(nearestEnemy);
            else BehaviourController.SwitchState<UnitIdleState>();
        }

        private void OnUnitAttack_UnityEditor(string attackType)
        {
            if (!Target.HasValue || !ReachedTarget()) return;

            float critRand = Random.value;
            float critMultiplier = critRand <= ProbabilityParams.CriticalChance ? BattleParams.CriticalMultiplier : 1;

            switch (attackType)
            {
                case "FastAttack":
                    Target.Value.Target.SetDamage((ushort)(BattleParams.FastAttackDamage * critMultiplier));
                    break;
                case "StrongAttack":
                    Target.Value.Target.SetDamage((ushort)(BattleParams.StrongAttackDamage * critMultiplier));
                    break;
            }
        }
        protected override void OnAnimationEnd_UnityEditor(string arg)
        {
            base.OnAnimationEnd_UnityEditor(arg);
            switch (arg)
            {
                case "Attack":
                    StartCoroutine(AttackCoroutine());
                    break;
            }
        }

        public void SetAttackAnimation(float rand)
        {
            if (rand <= ProbabilityParams.StrongAttackChance) Animator.SetTrigger("StrongAttack");
            else Animator.SetTrigger("FastAttack");
        }

        public override void Idle()
        {
            base.Idle();
            NavMeshAgent.destination = Position;
            NavMeshAgent.isStopped = true;
        }
        public virtual bool MoveTo(Vector3 point)
        {
            if (!CanMove || Dead) return false;

            Target = new TargetPoint(new Vector3(point.x, Position.y, point.z));
            NavMeshAgent.destination = point;
            NavMeshAgent.isStopped = false;
            Animator.SetFloat("Movement", 1f);
            Behaviour = UnitState.Move;
            BehaviourController.SwitchState<UnitMoveState>();
            return true;
        }
        public virtual bool MoveTo(Transform target)
        {
            if (!CanMove || Dead) return false;

            Target = new TargetPoint(target);
            NavMeshAgent.isStopped = false;
            Animator.SetFloat("Movement", 1f);
            Behaviour = UnitState.Move;
            BehaviourController.SwitchState<UnitSeekState>();
            return true;
        }
        public virtual bool MoveTo(BaseUnit target)
        {
            if (!CanMove || Dead || target.Dead) return false;

            Target = new TargetPoint(target);
            NavMeshAgent.isStopped = false;
            Animator.SetFloat("Movement", 1f);
            Behaviour = UnitState.Move;
            BehaviourController.SwitchState<UnitSeekState>();
            return true;
        }
        public virtual bool Attack(BaseUnit target)
        {
            if (Dead || target.Dead || target.Invulnerable) return false;

            if (Target.HasValue) Target.Value.Target.died -= OnTargetDied;
            target.died += OnTargetDied;
            Target = new TargetPoint(target);
            Behaviour = UnitState.Attack;
            BehaviourController.SwitchState<UnitAttackState>();
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
            BehaviourController.SwitchState<UnitWanderState>();
            return true;
        }

        public bool ReachedTarget()
        {
            if (!Target.HasValue) return false;
            if ((Target.Value.Target != null && Target.Value.Target.Dead) || Dead) return false;

            Vector3 targetPos = Target.Value.Position;
            targetPos.y = Position.y;
            float remainingDistance = (targetPos - Position).sqrMagnitude;
            float stoppingDistance = NavMeshAgent.stoppingDistance * NavMeshAgent.stoppingDistance;
            return remainingDistance <= stoppingDistance;
        }
    }
}