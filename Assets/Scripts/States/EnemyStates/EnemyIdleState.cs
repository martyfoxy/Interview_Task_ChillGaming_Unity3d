using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
using Zenject;

namespace Assets.Scripts.States.EnemyStates
{
    /// <summary>
    /// Состояние покоя противника
    /// </summary>
    public class EnemyIdleState : IState
    {
        private IEnemy _enemy;

        public EnemyIdleState(IEnemy enemy)
        {
            _enemy = enemy;
        }

        public void OnStart()
        {
        }

        public void Attack()
        {
            //Переход в состояние атаки
            _enemy.ChangeState(StatesEnum.Attack);
        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<EnemyIdleState>
        { }
    }
}
