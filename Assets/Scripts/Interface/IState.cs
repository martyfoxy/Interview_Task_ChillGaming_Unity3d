namespace Assets.Scripts.Interface
{
    /// <summary>
    /// Интерфейс всех состояний
    /// </summary>
    public interface IState
    {
        void OnStart();

        void Attack();
    }
}
