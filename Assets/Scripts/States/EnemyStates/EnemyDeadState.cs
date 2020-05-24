using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
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
    /// Состояние смерти противника
    /// </summary>
    public class EnemyDeadState : IState
    {
        private Enemy _enemy;

        public EnemyDeadState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void OnStart()
        {
            _enemy.EnemyAnimator.SetFloat("Health", _enemy.HP);
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
        public class Factory : PlaceholderFactory<EnemyDeadState>
        { }
    }
}
