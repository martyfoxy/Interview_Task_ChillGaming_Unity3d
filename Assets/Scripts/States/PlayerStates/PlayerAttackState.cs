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
        private Player _player;

        public PlayerAttackState(Player player)
        {
            _player = player;
        }

        public void OnStart()
        {
            //Анимация удара
            _player.PlayerAnimator.SetBool("Attack", true);

            //Нанесение урона
            //...
        }

        public void OnUpdate()
        {
            //Переход в состояние смерти
            if (_player.HP < 1)
                _player.ChangeState(StatesEnum.Dead);

            //Когда анимация перешла из атаки в покой, переходим в состояние покоя
            if (_player.PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("basePlayer_idle"))
                _player.ChangeState(StatesEnum.Idle);
        }

        public void OnDispose()
        {

        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<PlayerAttackState>
        { }
    }
}
