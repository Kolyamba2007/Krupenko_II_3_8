using UnityEngine;

namespace Ziggurat.Units
{
    public interface IUnit
    {
        string Name { get; }
        Vector3 Position { get; }
        Vector3 Target { get; }
        UnitType UnitType { get; }
    }
}
