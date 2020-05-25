using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
using Zenject;

namespace Assets.Scripts.States.PlayerStates
{
    /// <summary>
    /// Состояние игрока во время атаки
    /// </summary>
    public class PlayerAttackState : IState
    {
        private IPlayer _player;
        private IEnemy _enemy;

        public PlayerAttackState(IPlayer player, IEnemy enemy)
        {
            _player = player;
            _enemy = enemy;
        }

        public void OnStart()
        {
            //Анимация удара
            _player.GetAnimator().SetBool(GameConst.AttackParameter, true);

            Attack();
        }

        public void Attack()
        {
            //Нанесение урона
            var resDamage = _enemy.TakeDamage(_player.GetDamage());

            //Вампиризм
            _player.VampirismRestore(resDamage);

            //Переход в покой
            _player.ChangeState(StatesEnum.Idle);
        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<PlayerAttackState>
        { }
    }
}
