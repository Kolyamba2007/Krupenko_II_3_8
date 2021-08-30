namespace Ziggurat.Units
{
    public interface IManufacture
    {
        IUnit Produce<T>() where T : IUnit;
    }
}