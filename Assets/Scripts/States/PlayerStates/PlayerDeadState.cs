using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.Signals;
using Zenject;

namespace Assets.Scripts.States.PlayerStates
{
    /// <summary>
    /// Состояние смерти игрока
    /// </summary>
    public class PlayerDeadState : IState
    {
        private IPlayer _player;
        private SignalBus _signalBus;

        public PlayerDeadState(IPlayer player, SignalBus signalBus)
        {
            _player = player;
            _signalBus = signalBus;
        }

        public void OnStart()
        {
            //Анимация смерти
            _player.GetAnimator().SetInteger(GameConst.HealthParameter, 0);

            _signalBus.Fire(new EndOfGameSignal() { looser = _player });
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
