using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.States.PlayerStates;
using System;
using UnityEngine;

namespace Assets.Scripts.States.StateFactories
{
    /// <summary>
    /// Фабрика состояний игрока
    /// </summary>
    public class PlayerStateFactory : IStateFactory
    {
        //Ссылки на конкретные фабрики состояний
        private PlayerIdleState.Factory _idleFactory;
        private PlayerAttackState.Factory _attackFactory;
        private PlayerDeadState.Factory _deadFactory;

        public PlayerStateFactory(
            PlayerIdleState.Factory idleFactory, 
            PlayerAttackState.Factory attackFactory, 
            PlayerDeadState.Factory deadFactory)
        {
            _idleFactory = idleFactory;
            _attackFactory = attackFactory;
            _deadFactory = deadFactory;
        }

        public IState CreateState(StatesEnum state)
        {
            switch (state)
            {
                case StatesEnum.Idle:
                    {
                        return _idleFactory.Create();
                    }
                case StatesEnum.Attack:
                    {
                        return _attackFactory.Create();
                    }
                case StatesEnum.Dead:
                    {
                        return _deadFactory.Create();
                    }
                default:
                    throw new Exception("Неизвестное состояние");
            }
        }
    }
}
