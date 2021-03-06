﻿using Assets.Scripts.Models;
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

        /// <summary>
        /// Получить текущий урона
        /// </summary>
        /// <returns>Текущий урон</returns>
        float GetDamage();

        /// <summary>
        /// Получить текущую броню
        /// </summary>
        /// <returns>Текущая броня</returns>
        float GetArmor();

        /// <summary>
        /// Получить текущий вампиризм
        /// </summary>
        /// <returns>Текущий вампиризм</returns>
        float GetVampirism();

        /// <summary>
        /// Получить список баффов персонажа
        /// </summary>
        /// <returns>Список баффов</returns>
        List<Buff> GetBuffs();

        /// <summary>
        /// Атаковать
        /// </summary>
        void Attack();

        /// <summary>
        /// Получить урон
        /// </summary>
        /// <param name="damage">Количество урона</param>
        /// <returns>Результирующий полученный урон</returns>
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
        /// Задать начальные характеристики и добавить в игру
        /// </summary>
        /// <param name="startStats">Начальная стата</param>
        /// <param name="startBuffs">Начальные баффы</param>
        void BeginPlay(List<Stat> startStats, List<Buff> startBuffs);
    }
}
