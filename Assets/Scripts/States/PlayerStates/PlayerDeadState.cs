using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
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
            //Анимация смерти
            _player.GetAnimator().SetInteger(GameConst.HealthParameter, 0);
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
