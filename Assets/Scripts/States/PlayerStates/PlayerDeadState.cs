using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
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
    /// Состояние смерти игрока
    /// </summary>
    public class PlayerDeadState : IState
    {
        private Player _player;

        public PlayerDeadState(Player player)
        {
            _player = player;
        }

        public void OnStart()
        {
            _player.PlayerAnimator.SetFloat("Health", _player.HP);
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
        public class Factory : PlaceholderFactory<PlayerDeadState>
        { }
    }
}
