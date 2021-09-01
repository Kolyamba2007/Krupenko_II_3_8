using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.AI;

namespace Ziggurat.Units
{
    public class UnitBehaviourComponent : IStateSwitcher
    {
        private readonly BaseUnit Unit;
        private List<BaseState> States { set; get; } = new List<BaseState>();
        public BaseState CurrentState { private set; get; }
        
        public UnitBehaviourComponent(BaseUnit unit)
        {
            Unit = unit;
            CurrentState = new UnitIdleState(Unit, this);
        }

        public void Idle()
        {
            CurrentState.Idle();
        }
        public void Die()
        {
            CurrentState.Die();
        }
        public void Move(NavMeshAgent agent)
        {
            if (!Unit.Target.HasValue) return;

            float remainingDistance = (agent.destination - Unit.Position).sqrMagnitude;
            float stoppingDistance = agent.stoppingDistance * agent.stoppingDistance;
            if (remainingDistance <= stoppingDistance)
            {
                Unit.Idle();
                Idle();
                return;
            }
            agent.SetDestination(Unit.Target.Value.Position);
        }

        public void AddState<T>() where T : BaseState
        {
            States.Add((BaseState)Activator.CreateInstance(typeof(T), Unit, this));
        }
        public void SwitchState<T>() where T : BaseState
        {
            CurrentState.Stop();
            CurrentState = States.FirstOrDefault(state => state is T);
            CurrentState.Start();
        }       
    }
}