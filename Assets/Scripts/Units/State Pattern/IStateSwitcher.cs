namespace Ziggurat.Units
{
    public interface IStateSwitcher
    {
        bool IsActive { get; }
        BaseState CurrentState { get; }
        void Start();
        void Stop();
        void SwitchState<T>() where T : BaseState;
    }
}