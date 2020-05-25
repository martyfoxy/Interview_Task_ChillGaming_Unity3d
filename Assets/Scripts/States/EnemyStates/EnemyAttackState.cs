using Assets.Scripts.Interface;
using Assets.Scripts.Models.Enums;
using Zenject;

namespace Assets.Scripts.States.EnemyStates
{
    /// <summary>
    /// Состояние атаки противника
    /// </summary>
    public class EnemyAttackState : IState
    {
        private IEnemy _enemy;
        private IPlayer _player;

        public EnemyAttackState(IEnemy enemy, IPlayer player)
        {
            _enemy = enemy;
            _player = player;
        }

        public void OnStart()
        {
            //Анимация удара
            _enemy.GetAnimator().SetBool(GameConst.AttackParameter, true);

            Attack();
        }

        public void Attack()
        {
            //Нанесение урона
            var resDamage = _player.TakeDamage(_enemy.GetDamage());

            //Вампиризм
            _enemy.VampirismRestore(resDamage);

            //Переход в покой
            _enemy.ChangeState(StatesEnum.Idle);
        }

        /// <summary>
        /// Конкретная фабрика
        /// </summary>
        public class Factory : PlaceholderFactory<EnemyAttackState>
        { }
    }
}
