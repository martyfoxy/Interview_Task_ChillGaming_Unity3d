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
    /// Состояние смерти игрока
    /// </summary>
    public class PlayerDeadState : IState
    {
        private IPlayer _player;

        public PlayerDeadState(IPlayer player)
        {
            _player = player;
        }

        public void OnStart()
        {
            _player.GetAnimator().SetInteger(AnimationParametersConst.HealthParameter, 0);
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
        public class Factory : PlaceholderFactory<PlayerDeadState>
        { }
    }
}
