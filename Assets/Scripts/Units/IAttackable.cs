namespace Ziggurat.Units
{
    public interface IAttackable
    {
        float AttackCooldown { get; }
        bool Attack(BaseUnit unit);
    }
}