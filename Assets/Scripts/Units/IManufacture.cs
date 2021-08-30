using Ziggurat.UI;

namespace Ziggurat.Units
{
    public interface IManufacture
    {
        bool IsManufacturing { get; }
        float ProductionProgress { get; }

        IUnit ProduceUnit<T>() where T : IUnit;
        void Abort();
    }
}