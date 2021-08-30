using UnityEngine;

namespace Ziggurat.Units
{
    public abstract class BaseUnit : MonoBehaviour, IUnit
    {
        public string Name => transform.name;
        public Vector3 Position { protected set; get; }
        public Vector3 Target { protected set; get; }
        public abstract UnitType UnitType { get; }
    }
}