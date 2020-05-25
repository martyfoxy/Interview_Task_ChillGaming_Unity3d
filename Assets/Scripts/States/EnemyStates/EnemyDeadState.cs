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
    /// Состояние смерти противника
    /// </summary>
    public class EnemyDeadState : IState
    {
        private IEnemy _enemy;

        public EnemyDeadState(IEnemy enemy)
        {
            _enemy = enemy;
        }

        public void OnStart()
        {
            var animator = _enemy.GetAnimator();
            animator.SetInteger(AnimationParametersConst.HealthParameter, 0);
        }

        public void OnUpdate()
        {

        }

        public void OnDispose()
        {

        }

        public void Attack()
        {
            //Ничего не делаем
        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<EnemyDeadState>
        { }
    }
}
