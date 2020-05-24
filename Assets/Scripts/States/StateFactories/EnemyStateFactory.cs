using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.States.EnemyStates;
using System;

namespace Assets.Scripts.States.StateFactories
{
    /// <summary>
    /// Фабрика состояний противника
    /// </summary>
    public class EnemyStateFactory : IStateFactory
    {
        //Ссылки на конкретные фабрики состояний
        private EnemyIdleState.Factory _idleFactory;
        private EnemyAttackState.Factory _attackFactory;
        private EnemyDeadState.Factory _deadFactory;

        public EnemyStateFactory(
            EnemyIdleState.Factory idleFactory,
            EnemyAttackState.Factory attackFactory,
            EnemyDeadState.Factory deadFactory
            )
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
