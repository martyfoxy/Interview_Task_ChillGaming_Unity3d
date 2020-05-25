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
    /// Состояние покоя противника
    /// </summary>
    public class EnemyIdleState : IState
    {
        private IEnemy _enemy;

        public EnemyIdleState(IEnemy enemy)
        {
            _enemy = enemy;
        }

        public void OnStart()
        {
        }

        public void OnUpdate()
        {
            //Переход с состояние смерти
            if (_enemy.GetHP() < 1)
                _enemy.ChangeState(StatesEnum.Dead);
        }

        public void OnDispose()
        {

        }

        public void Attack()
        {
            //Переход в состояние атаки
            _enemy.ChangeState(StatesEnum.Attack);
        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<EnemyIdleState>
        { }
    }
}
