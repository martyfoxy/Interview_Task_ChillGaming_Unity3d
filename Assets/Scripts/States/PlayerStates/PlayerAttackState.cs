using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.States.StateFactories;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.PlayerStates
{
    /// <summary>
    /// Состояние игрока во время атаки
    /// </summary>
    public class PlayerAttackState : IState
    {
        private IPlayer _player;
        private IEnemy _enemy;

        public PlayerAttackState(IPlayer player, IEnemy enemy)
        {
            _player = player;
            _enemy = enemy;
        }

        public void OnStart()
        {
            //Анимация удара
            _player.GetAnimator().SetBool(AnimationParametersConst.AttackParameter, true);

            Attack();
        }

        public void OnUpdate()
        {
            //Переход в состояние смерти
            if (_player.GetHP() < 1)
                _player.ChangeState(StatesEnum.Dead);
        }

        public void OnDispose()
        {

        }

        public void Attack()
        {
            //Нанесение урона
            var damage = _enemy.TakeDamage(_player.GetDamage());

            //Вампиризм
            _player.VampirismRestore(damage);

            //Переход в покой
            _player.ChangeState(StatesEnum.Idle);
        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<PlayerAttackState>
        { }
    }
}
