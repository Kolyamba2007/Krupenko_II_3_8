namespace Ziggurat.Units
{
    public interface IStateSwitcher
    {
        BaseState CurrentState { get; }
        void SwitchState<T>() where T : BaseState;
    }
}