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

        IUnit ProduceUnit<T>() where T : IUnit;
        void Abort();
    }
}