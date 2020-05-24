using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interface
{
    /// <summary>
    /// Интерфейс всех персонажей
    /// </summary>
    public interface ICharacter
    {
        /// <summary>
        /// Получить текущее HP
        /// </summary>
        /// <returns>Текущее HP</returns>
        float GetHP();

        /// <summary>
        /// Атаковать
        /// </summary>
        void Attack();

        /// <summary>
        /// Получить урон
        /// </summary>
        /// <param name="damage">Количество урона</param>
        void TakeDamage(float damage);
    }
}
