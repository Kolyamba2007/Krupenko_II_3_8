using UnityEngine;

namespace Ziggurat.Units
{
    public interface IMovable
    {
        bool CanMove { get; }
        float MovementSpeed { get; }

        bool MoveTo(Vector3 point);
        bool MoveTo(Transform target);
        bool MoveTo(BaseUnit target);
    }
}