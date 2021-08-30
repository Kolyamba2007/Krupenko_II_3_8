using UnityEngine;

namespace Ziggurat.Units
{
    [DisallowMultipleComponent]
    //[RequireComponent(typeof(Collider))]
    //[RequireComponent(typeof(Animator))]
    public abstract class BaseUnit : MonoBehaviour, IUnit
    {
        public string Name => transform.name;
        public Vector3 Position { protected set; get; }
        public Vector3 Target { protected set; get; }
        public abstract UnitType UnitType { get; }

        #region Statuses
        [Header("Статусы")]
        [SerializeField]
        private bool _paused;
        [SerializeField]
        private bool _invincible;
        public bool Paused { protected set => _paused = value; get => _paused; }
        public bool Invincible { protected set => _invincible = value; get => _invincible; }
        #endregion

        #region Characteristics
        [Header("Характеристики")]
        [SerializeField]
        private byte _health;
        public byte Health { protected set => _health = value; get => _health; }
        #endregion
    }
}