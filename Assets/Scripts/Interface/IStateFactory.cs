using Assets.Scripts.Models.Enums;
using Assets.Scripts.States;

namespace Assets.Scripts.Interface
{
    /// <summary>
    /// Интерфейс фабрик состояний
    /// </summary>
    public interface IStateFactory
    {
        /// <summary>
        /// Создать класс состояния
        /// </summary>
        /// <param name="state">Состояние</param>
        /// <returns>Экземпляр класса состояния</returns>
        IState CreateState(StatesEnum state);
    }
}
