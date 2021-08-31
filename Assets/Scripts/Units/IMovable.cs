using UnityEngine;
using UnityEngine.AI;

namespace Ziggurat.Units
{
    public interface IMovable
    {
        bool CanMove { get; }
        Vector3 Velocity { get; }

        bool MoveTo(Vector3 point);
        bool MoveTo(Transform target);
        bool MoveTo(IUnit target);
    }
}