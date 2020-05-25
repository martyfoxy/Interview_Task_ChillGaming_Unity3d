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
        private IPlayer _player;

        public PlayerIdleState(IPlayer player)
        {
            _player = player;
        }

        public void OnStart()
        {
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
            //Переход в состояние атаки
            _player.ChangeState(StatesEnum.Attack);
        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<PlayerIdleState>
        { }
    }
}
