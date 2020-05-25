using Assets.Scripts.Models;
using Assets.Scripts.Models.Enums;
using System.Collections.Generic;
using UnityEngine;

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

        float GetDamage();

        float GetArmor();

        float GetVampirism();

        List<Stat> GetStats();

        List<Buff> GetBuffs();


        /// <summary>
        /// Атаковать
        /// </summary>
        void Attack();

        /// <summary>
        /// Получить урон
        /// </summary>
        /// <param name="damage">Количество урона</param>
        /// <returns>Результирующий урон</returns>
        float TakeDamage(float damage);

        /// <summary>
        /// Восстановить HP, в зависимости от нанесенного урона и вампиризма
        /// </summary>
        /// <param name="damage">Нанесенный урон</param>
        void VampirismRestore(float damage);

        /// <summary>
        /// Получить аниматор персонажа
        /// </summary>
        /// <returns>Аниматор</returns>
        Animator GetAnimator();

        /// <summary>
        /// Сменить состояние персонажа
        /// </summary>
        /// <param name="state">Новое состояние</param>
        void ChangeState(StatesEnum state);

        /// <summary>
        /// Задать начальные характеристики
        /// </summary>
        /// <param name="startStats">Начальная стата</param>
        /// <param name="startBuffs">Начальные баффы</param>
        void SetDefault(List<Stat> startStats, List<Buff> startBuffs);
    }
}
