using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ziggurat.Units;

public class GameManager : MonoBehaviour
{
    LinkedList<IUnit> units = new LinkedList<IUnit>();

    void AddUnit(IUnit unit)
    {
        if(units.Contains(unit))
            units.AddLast(unit);
    }

    void RemoveUnit(IUnit unit)
    {
        units.Remove(unit);
    }

    IUnit ClosestEnemy(IUnit unit)
    {
        IUnit enemy = null;
        float minDist = float.MaxValue;

        foreach(IUnit e in units)
        {
            float dist = Vector3.Distance(unit.Position, e.Position);

            if (unit != e && dist < minDist)
            {
                minDist = dist;
                enemy = e;
            }
        }

        return enemy;
    }
}
