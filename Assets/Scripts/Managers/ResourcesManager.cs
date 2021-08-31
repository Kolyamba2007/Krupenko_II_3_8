using System;
using UnityEngine;
using Ziggurat.Units;

namespace Ziggurat.Managers
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _knightPrefab;
        [SerializeField]
        private GameObject _zigguratPrefab;

        public GameObject GetUnitByType<T>() where T : IUnit
        {
            if (typeof(T).IsAbstract) throw new Exception();

            if (typeof(T).IsAssignableFrom(typeof(KnightScript))) return _knightPrefab;
            if (typeof(T).IsAssignableFrom(typeof(ZigguratScript))) return _zigguratPrefab;
            throw new Exception();
        }
    }
}