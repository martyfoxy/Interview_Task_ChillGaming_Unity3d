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
        private Enemy _enemy;

        public EnemyIdleState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void OnStart()
        {
            _enemy.EnemyAnimator.SetBool("Attack", false);
        }

        public void OnUpdate()
        {
            //Переход с состояние смерти
            if (_enemy.HP < 1)
                _enemy.ChangeState(StatesEnum.Dead);
        }

        public void OnDispose()
        {

        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<EnemyIdleState>
        { }
    }
}
