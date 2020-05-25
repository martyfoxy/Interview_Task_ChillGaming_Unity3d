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
        private IEnemy _enemy;

        public EnemyAttackState(IEnemy enemy)
        {
            _enemy = enemy;
        }

        public void OnStart()
        {
            //Анимация удара
            _enemy.GetAnimator().SetBool(AnimationParametersConst.AttackParameter, true);

            Attack();
        }

        public void OnUpdate()
        {
            //Переход в состояние смерти
            if (_enemy.GetHP() < 1)
                _enemy.ChangeState(StatesEnum.Dead);
        }

        public void OnDispose()
        {

        }

        public void Attack()
        {
            //Нанесение урона
            //...
            Debug.Log("ATTACK");

            //Переход в покой
            _enemy.ChangeState(StatesEnum.Idle);
        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<EnemyAttackState>
        { }
    }
}
