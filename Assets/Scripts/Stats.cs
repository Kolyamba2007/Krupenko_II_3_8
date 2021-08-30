using OneLine;
using System;
using UnityEngine;

namespace Ziggurat.Units
{
    /// <summary>
    /// Класс, описывающий различные параметры юнита
    /// </summary>
    [Serializable]
    public class StatsData
    {
        public BaseParamsData BaseParams = BaseParamsData.Empty;
        public MobilityParamsData MobilityParams = MobilityParamsData.Empty;
        public BattleParamsData BattleParams = BattleParamsData.Empty;
        public ProbabilityParamsData ProbabilityParams = ProbabilityParamsData.Empty;

        public static StatsData operator +(StatsData a, StatsData b)
        {
            var sum = new StatsData();

            var baseParams = a.BaseParams;
            baseParams.MaxHealth += b.BaseParams.MaxHealth;
            baseParams.HPRegenPerSec += b.BaseParams.HPRegenPerSec;
            sum.BaseParams = baseParams;

            var mobilityParams = a.MobilityParams;
            mobilityParams.MoveSpeed = Mathf.Clamp(mobilityParams.MoveSpeed + b.MobilityParams.MoveSpeed, 0f, 100f);
            mobilityParams.JumpForce = Mathf.Clamp(mobilityParams.JumpForce + b.MobilityParams.JumpForce, 0f, 100f);
            sum.MobilityParams = mobilityParams;

            var battleParams = a.BattleParams;
            battleParams.CriticalMultiplier = Mathf.Clamp(battleParams.CriticalMultiplier + b.BattleParams.CriticalMultiplier, 0f, 0.95f);
            battleParams.FastAttackDamage += b.BattleParams.FastAttackDamage;
            battleParams.StrongAttackDamage += b.BattleParams.StrongAttackDamage;
            sum.BattleParams = battleParams;

            var probabilityParamsData = a.ProbabilityParams;
            probabilityParamsData.CriticalChance = Mathf.Clamp(probabilityParamsData.CriticalChance + b.ProbabilityParams.CriticalChance, 0f, 1f);
            probabilityParamsData.MissChance = Mathf.Clamp(probabilityParamsData.MissChance - b.ProbabilityParams.MissChance, 0f, 1f);
            sum.ProbabilityParams = probabilityParamsData;

            return sum;
        }
    }

    [Serializable]
    public struct BaseParamsData
    {
        /// <summary>
        /// Здоровье, сколько урона выдержит персонаж
        /// </summary>
        [Min(0), Tooltip("Здоровье, сколько урона выдержит персонаж")]
        public float MaxHealth;
        /// <summary>
        /// Скорость восстановления здоровья в секунду
        /// </summary>
        [Min(0), Tooltip("Скорость восстановления здоровья в секунду")]
        public float HPRegenPerSec;

        public static BaseParamsData Empty => new BaseParamsData()
        {
            MaxHealth = 10f,
            HPRegenPerSec = 0f
        };

        public override bool Equals(object obj)
        {
            if (!(obj is BaseParamsData)) return false;
            var data = (BaseParamsData)obj;
            return data.MaxHealth == MaxHealth && data.HPRegenPerSec == HPRegenPerSec;
        }
    }

    /// <summary>
    /// Физические параметры юнита
    /// </summary>
    [Serializable]
    public struct MobilityParamsData
    {
        /// <summary>
        /// Скорость перемещения
        /// </summary>
        [Min(0), Tooltip("Скорость перемещения")]
        public float MoveSpeed;
        /// <summary>
        /// Скорость поворота
        /// </summary>
        [Min(0), Tooltip("Скорость поворота")]
        public float RotateSpeed;
        /// <summary>
        /// Сила прыжка
        /// </summary>
        [Min(0), Tooltip("Сила прыжка")]
        public float JumpForce;

        public static MobilityParamsData Empty => new MobilityParamsData()
        {
            MoveSpeed = 1f,
            RotateSpeed = 1f,
            JumpForce = 5f
        };

        public override bool Equals(object obj)
        {
            if (!(obj is MobilityParamsData)) return false;

            var data = (MobilityParamsData)obj;

            return data.MoveSpeed == MoveSpeed &&
                data.JumpForce == JumpForce &&
                data.RotateSpeed == RotateSpeed;
        }
    }

    /// <summary>
    /// Боевые параметры юнита
    /// </summary>
    [Serializable]
    public struct BattleParamsData
    {
        /// <summary>
        /// Урон при быстрых атаках
        /// </summary>
        [Min(0), Tooltip("Урон при быстрых атаках")]
        public float FastAttackDamage;
        /// <summary>
        /// Урон при сильных атаках
        /// </summary>
        [Min(0), Tooltip("Урон при сильных атаках")]
        public float StrongAttackDamage;
        /// <summary>
        /// Множитель критического урона
        /// </summary>
        [Min(0), Tooltip("Множитель критического урона")]
        public float CriticalMultiplier;
        /// <summary>
        /// Перезарядка.
        /// </summary>
        [Min(0), Tooltip("Перезарядка")]
        public float AttackCooldown;

        public static BattleParamsData Empty => new BattleParamsData()
        {
            FastAttackDamage = 5f,
            StrongAttackDamage = 10f,
            CriticalMultiplier = 1f,
            AttackCooldown = 1.5f
        };

        public override bool Equals(object obj)
        {
            if (!(obj is BattleParamsData)) return false;

            var data = (BattleParamsData)obj;

            return data.FastAttackDamage == FastAttackDamage &&
                   data.StrongAttackDamage == StrongAttackDamage &&
                   data.CriticalMultiplier == CriticalMultiplier &&
                   data.AttackCooldown == AttackCooldown;
        }
    }

    /// <summary>
    /// Вероятностные параметры юнита
    /// </summary>
    [Serializable]
    public struct ProbabilityParamsData
    {
        /// <summary>
        /// Шанс критической атаки
        /// </summary>
        [Min(0), Tooltip("Шанс критической атаки")]
        public float CriticalChance;

        /// <summary>
        /// Шанс промаха
        /// </summary>
        [Min(0), Tooltip("Шанс промаха")]
        public float MissChance;

        /// <summary>
        /// Шанс нанесения сильной атаки
        /// </summary>
        [Min(0), Tooltip("Шанс нанесения сильной атаки")]
        public float StrongAttackChance;

        public static ProbabilityParamsData Empty => new ProbabilityParamsData()
        {
            CriticalChance = 0.05f,
            MissChance = 0.25f,
            StrongAttackChance = 0.35f
        };

        public override bool Equals(object obj)
        {
            if (!(obj is ProbabilityParamsData)) return false;

            var data = (ProbabilityParamsData)obj;

            return data.CriticalChance == CriticalChance &&
                   data.MissChance == MissChance &&
                   data.StrongAttackChance == StrongAttackChance;
        }
    }
}