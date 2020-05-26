using Assets.Scripts.Signals;

namespace Assets.Scripts.Interface
{
    /// <summary>
    /// Интерфейс всех UIManager
    /// </summary>
    public interface IUIManager
    {
        /// <summary>
        /// Очистить панели игроков
        /// </summary>
        void Reset();

        /// <summary>
        /// Отобразить текущие характеристики игроков
        /// </summary>
        void ShowStatsIcons();

        /// <summary>
        /// Отобразить иконки баффов игроков
        /// </summary>
        void ShowBuffsIcons();

        /// <summary>
        /// Обработчик сигнала изменения показаний игроков
        /// </summary>
        /// <param name="signal">Сигнал</param>
        void UpdateValue(CharacterStatChangedSignal signal);

        /// <summary>
        /// Обработчик сигнала окончания игры
        /// </summary>
        /// <param name="signal">Сигнал</param>
        void EndOfGame(EndOfGameSignal signal);
    }
}
