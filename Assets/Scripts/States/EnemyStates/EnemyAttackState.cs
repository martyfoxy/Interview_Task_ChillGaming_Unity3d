using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.EnemyStates
{
    /// <summary>
    /// Состояние атаки противника
    /// </summary>
    public class EnemyAttackState : IState
    {
        private Enemy _enemy;

        public EnemyAttackState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void OnStart()
        {
            //Анимация удара
            _enemy.EnemyAnimator.SetBool("Attack", true);

            //Нанесение урона
            //...

            //Переход в состояние покоя
            _enemy.ChangeState(StatesEnum.Idle);
        }

        public void OnUpdate()
        {

        }

        public void OnDispose()
        {

        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<EnemyAttackState>
        { }
    }
}
