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

        BaseMelee ProduceUnit<T>() where T : BaseMelee;
        void Abort();
    }
}