using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.Signals;
using Zenject;

namespace Assets.Scripts.States.EnemyStates
{
    /// <summary>
    /// Состояние смерти противника
    /// </summary>
    public class EnemyDeadState : IState
    {
        private IEnemy _enemy;
        private SignalBus _signalBus;

        public EnemyDeadState(IEnemy enemy, SignalBus signalBus)
        {
            _enemy = enemy;
            _signalBus = signalBus;
        }

        public void OnStart()
        {
            //Анимация смерти
            _enemy.GetAnimator().SetInteger(GameConst.HealthParameter, 0);

            _signalBus.Fire(new EndOfGameSignal() { looser = _enemy });
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
