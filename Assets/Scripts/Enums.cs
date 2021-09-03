using System;

namespace Ziggurat
{
    /// <summary>
    /// Виды действий
    /// </summary>
    public enum ActionType : byte
    {
        /// <summary>
        /// Ожидание
        /// </summary>
        Idle = 0,
        /// <summary>
        /// Ходьба
        /// </summary>
        Move = 1,
        /// <summary>
        /// Быстрая атака
        /// </summary>
        FastAttack = 20,
        /// <summary>
        /// Сильная атака
        /// </summary>
        StrongAttack = 21,
        /// <summary>
        /// Контратака
        /// </summary>
        CounterAttack = 24,
        /// <summary>
        /// Блокирование
        /// </summary>
        Block = 40,
        /// <summary>
        /// Атакующая способность
        /// </summary>
        AttackAbility = 60,
        Die = 65,
        Impact = 70,
    }

    /// <summary>
    /// Типы поведения
    /// </summary>
    public enum UnitState : byte
    {
        Idle = 0,
        Move = 1,
        Seek = 2,
        Attack = 3,
        Wander = 4,
        Die = 5,
    }

    /// <summary>
    /// Типы дистанции
    /// </summary>
    public enum DistanceType : byte
    {
        /// <summary>
        /// Неопределенная
        /// </summary>
        Indefinite = 0,
        /// <summary>
        /// Тесное взаимодействие
        /// </summary>
        Melee = 1,
        /// <summary>
        /// Дальняя конфронтация
        /// </summary>
        Ranged = 2,
        /// <summary>
        /// Свободное расстояние
        /// </summary>
        Spell = 3
    }

    /// <summary>
    /// Типы юнитов
    /// </summary>
    public enum UnitType : byte
    {
        /// <summary>
        /// Неопределенный тип юнита
        /// </summary>
        None = 0,
        /// <summary>
        /// Юнит ближнего боя
        /// </summary>
        Melee = 1,
        /// <summary>
        /// Юнит дальнего боя
        /// </summary>
        Range = 2,
        /// <summary>
        /// Воздушная боевая единица
        /// </summary>
        Flying = 3,
        /// <summary>
        /// Строение
        /// </summary>
        Building = 4,
    }

    /// <summary>
    /// Типы результатов действия
    /// </summary>
    public enum ActionResultType : byte
    {
        /// <summary>
        /// Неопределённый результат
        /// </summary>
        None,
        /// <summary>
        /// Прервано
        /// </summary>
        Interrupted,
        /// <summary>
        /// Неудачно
        /// </summary>
        Failed,
        /// <summary>
        /// Успешно
        /// </summary>
        Successfully,
        /// <summary>
        /// Завершен
        /// </summary>
        Completed,
    }
    public enum AttackResultType
    {
        /// <summary>
        /// Неопределённый результат
        /// </summary>
        None,
        /// <summary>
        /// Был совершён промах
        /// </summary>
        Missed,
        /// <summary>
        /// Обычный удар
        /// </summary>
        Normal,
        /// <summary>
        /// Был нанесён критический удар
        /// </summary>
        Critical
    }

    /// <summary>
    /// Владелец юнита.
    /// </summary>
    public enum Owner { Red, Green, Blue, Neutral }
}