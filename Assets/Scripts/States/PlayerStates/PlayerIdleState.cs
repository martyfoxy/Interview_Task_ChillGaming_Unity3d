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

namespace Assets.Scripts.States.PlayerStates
{
    /// <summary>
    /// Состояние покоя игрока
    /// </summary>
    public class PlayerIdleState : IState
    {
        private Player _player;

        public PlayerIdleState(Player player)
        {
            _player = player;
        }

        public void OnStart()
        {
            _player.PlayerAnimator.SetBool("Attack", false);
        }

        public void OnUpdate()
        {
            //Переход в состояние смерти
            if (_player.HP < 1)
                _player.ChangeState(StatesEnum.Dead);
        }

        public void OnDispose()
        {

        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<PlayerIdleState>
        { }
    }
}
