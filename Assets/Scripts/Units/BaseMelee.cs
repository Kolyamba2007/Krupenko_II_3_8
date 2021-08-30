using UnityEngine;

namespace Ziggurat.Units
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseMelee : BaseUnit, IMovable
    {
        public override UnitType UnitType => UnitType.Melee;

        private Animator Animator { set; get; }
        private Rigidbody Rigidbody { set; get; }

        public bool CanMove { protected set; get; }

        public Vector3 Velocity { protected set; get; }

        public bool MoveTo(Vector3 point)
        {
            throw new System.NotImplementedException();
        }
        public bool MoveTo(Transform target)
        {
            throw new System.NotImplementedException();
        }
        public bool MoveTo(IUnit target)
        {
            throw new System.NotImplementedException();
        }
    }
}