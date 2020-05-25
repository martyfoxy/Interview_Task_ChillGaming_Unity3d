using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
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
