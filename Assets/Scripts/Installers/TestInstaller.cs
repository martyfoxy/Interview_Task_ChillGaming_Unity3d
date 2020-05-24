using Assets.Scripts.Interface;
using Assets.Scripts.Managers;
using Assets.Scripts.Mocks;
using Assets.Scripts.States.EnemyStates;
using Assets.Scripts.States.PlayerStates;
using Assets.Scripts.States.StateFactories;
using Zenject;

namespace Assets.Scripts.Installers
{
    /// <summary>
    /// Установщик DI для юнит-тестирования
    /// </summary>
    public class TestInstaller : Installer<TestInstaller>
    {
        public override void InstallBindings()
        {
            //Биндим mock игрового менеджера
            Container.Bind<IGameManager>().To<MockGameManager>().AsSingle();

            //Биндим фабрику состояний игрока
            Container.BindInterfacesAndSelfTo<PlayerStateFactory>().AsSingle();

            //Биндим конкретные фабрики каждого состояния
            Container.BindFactory<PlayerIdleState, PlayerIdleState.Factory>().WhenInjectedInto<PlayerStateFactory>();
            Container.BindFactory<PlayerAttackState, PlayerAttackState.Factory>().WhenInjectedInto<PlayerStateFactory>();
            Container.BindFactory<PlayerDeadState, PlayerDeadState.Factory>().WhenInjectedInto<PlayerStateFactory>();

            //Бндним фабрику состояний противника
            Container.BindInterfacesAndSelfTo<EnemyStateFactory>().AsSingle();

            //Биндим конкретные фабрики каждого состояния
            Container.BindFactory<EnemyIdleState, EnemyIdleState.Factory>().WhenInjectedInto<EnemyStateFactory>();
            Container.BindFactory<EnemyAttackState, EnemyAttackState.Factory>().WhenInjectedInto<EnemyStateFactory>();
            Container.BindFactory<EnemyDeadState, EnemyDeadState.Factory>().WhenInjectedInto<EnemyStateFactory>();
        }
    }
}
