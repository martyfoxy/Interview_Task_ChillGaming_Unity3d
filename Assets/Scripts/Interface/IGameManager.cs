using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interface
{
    public interface IGameManager
    {
        /// <summary>
        /// Начать игру без бафов
        /// </summary>
        void StartDefault();

        /// <summary>
        /// Начать игру с бафами
        /// </summary>
        void StartWithBuffs();
    }
}
