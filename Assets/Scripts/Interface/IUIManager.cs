using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
