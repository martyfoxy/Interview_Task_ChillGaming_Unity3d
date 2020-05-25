using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
using Zenject;

namespace Assets.Scripts.States.EnemyStates
{
    /// <summary>
    /// Состояние смерти противника
    /// </summary>
    public class EnemyDeadState : IState
    {
        private IEnemy _enemy;

        public EnemyDeadState(IEnemy enemy)
        {
            _enemy = enemy;
        }

        public void OnStart()
        {
            //Анимация смерти
            _enemy.GetAnimator().SetInteger(GameConst.HealthParameter, 0);
        }

        public void Attack()
        {
            //Ничего не делаем
        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<EnemyDeadState>
        { }
    }
}
