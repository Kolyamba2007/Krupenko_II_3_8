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

        public GameObject GetUnitByType(Type type)
        {
            if (type.IsAbstract) throw new Exception();

            if (type.IsAssignableFrom(typeof(KnightScript))) return _knightPrefab;
            if (type.IsAssignableFrom(typeof(ZigguratScript))) return _zigguratPrefab;
            throw new Exception();
        }
    }
}