using Assets.Scripts.Managers;
using Assets.Scripts.Signals;
using Assets.Scripts.States.EnemyStates;
using Assets.Scripts.States.PlayerStates;
using Assets.Scripts.States.StateFactories;
using Zenject;

namespace Assets.Scripts.Installers
{
    /// <summary>
    /// Установщик DI для игры
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallCore();
            InstallPlayer();
            InstallEnemy();
        }

        /// <summary>
        /// Главная инициализация
        /// </summary>
        private void InstallCore()
        {
            SignalBusInstaller.Install(Container);

            //Биндим сигналы
            Container.DeclareSignal<CharacterStatChangedSignal>();
            Container.DeclareSignal<EndOfGameSignal>();
            Container.BindSignal<CharacterStatChangedSignal>().ToMethod<UIManager>(x => x.UpdateValue).FromResolve();
            Container.BindSignal<EndOfGameSignal>().ToMethod<UIManager>(x => x.EndOfGame).FromResolve();

            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        }

        /// <summary>
        /// Инициализация игрока
        /// </summary>
        private void InstallPlayer()
        {
            //Биндим фабрику состояний игрока
            Container.BindInterfacesAndSelfTo<PlayerStateFactory>().AsSingle();

            //Биндим конкретные фабрики каждого состояния
            Container.BindFactory<PlayerIdleState, PlayerIdleState.Factory>().WhenInjectedInto<PlayerStateFactory>();
            Container.BindFactory<PlayerAttackState, PlayerAttackState.Factory>().WhenInjectedInto<PlayerStateFactory>();
            Container.BindFactory<PlayerDeadState, PlayerDeadState.Factory>().WhenInjectedInto<PlayerStateFactory>();
        }

        /// <summary>
        /// Инициализация противника
        /// </summary>
        public void InstallEnemy()
        {
            //Бндним фабрику состояний противника
            Container.BindInterfacesAndSelfTo<EnemyStateFactory>().AsSingle();

            //Биндим конкретные фабрики каждого состояния
            Container.BindFactory<EnemyIdleState, EnemyIdleState.Factory>().WhenInjectedInto<EnemyStateFactory>();
            Container.BindFactory<EnemyAttackState, EnemyAttackState.Factory>().WhenInjectedInto<EnemyStateFactory>();
            Container.BindFactory<EnemyDeadState, EnemyDeadState.Factory>().WhenInjectedInto<EnemyStateFactory>();
        }
    }
}
