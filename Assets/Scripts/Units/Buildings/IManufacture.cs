using System;
using UnityEngine;

namespace Ziggurat.Units
{
    public interface IManufacture
    {
        /// <summary>
        /// Производит в данныц момент юнита
        /// </summary>
        bool IsManufacturing { get; }

        /// <summary>
        /// Текущее значение прогресса
        /// </summary>
        float ProductionProgress { get; }

        /// <summary>
        /// Время производства
        /// </summary>
        float ProductionTime { get; }

        /// <summary>
        /// Точка создания юнита
        /// </summary>
        Transform SpawnPoint { get; }

        /// <summary>
        /// Начинает производство юнита указанного типа
        /// </summary>
        /// <typeparam name="T">Тип юнита</typeparam>
        /// <returns></returns>
        void ProduceUnit<T>() where T : BaseMelee;

        /// <summary>
        /// Прерывает текущее производство юнита
        /// </summary>
        void Abort();

        event Action<Type> Manufactured;
    }
}