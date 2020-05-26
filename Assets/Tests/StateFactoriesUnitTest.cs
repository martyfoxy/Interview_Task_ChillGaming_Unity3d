using Zenject;
using NUnit.Framework;
using Assets.Scripts.Installers;
using Assets.Scripts.Interface;
using Assets.Scripts.States.StateFactories;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.Mocks;
using Assets.Scripts.States.PlayerStates;
using Assets.Scripts.States.EnemyStates;
using Assets.Scripts.Signals;

[TestFixture]
public class StateFactoriesUnitTest : ZenjectUnitTestFixture
{
    [SetUp]
    public void BindInterfaces()
    {
        //Запускаем тестовый установщик
        TestInstaller.Install(Container);

        //Mock персонажей
        Container.Bind<IPlayer>().To<MockPlayer>().AsSingle();
        Container.Bind<IEnemy>().To<MockEnemy>().AsSingle();
    }

    [Test]
    public void PlayerFactoryResolved()
    {
        PlayerStateFactory stateFactory = Container.Resolve<PlayerStateFactory>();

        Assert.NotNull(stateFactory);
    }

    [Test]
    public void PlayerFactoryCreatedIdleState()
    {
        var factory = Container.Resolve<PlayerStateFactory>();
        var state = factory.CreateState(StatesEnum.Idle);

        Assert.IsTrue(state is PlayerIdleState);
    }

    [Test]
    public void PlayerFactoryCreatedAttackState()
    {
        var factory = Container.Resolve<PlayerStateFactory>();
        var state = factory.CreateState(StatesEnum.Attack);

        Assert.IsTrue(state is PlayerAttackState);
    }

    [Test]
    public void PlayerFactoryCreatedDeadState()
    {
        var factory = Container.Resolve<PlayerStateFactory>();
        var state = factory.CreateState(StatesEnum.Dead);

        Assert.IsTrue(state is PlayerDeadState);
    }

    [Test]
    public void EnemyFactoryResolved()
    {
        EnemyStateFactory stateFactory = Container.Resolve<EnemyStateFactory>();

        Assert.NotNull(stateFactory);
    }

    [Test]
    public void EnemyFactoryCreatedIdleState()
    {
        var factory = Container.Resolve<EnemyStateFactory>();
        var state = factory.CreateState(StatesEnum.Idle);

        Assert.IsTrue(state is EnemyIdleState);
    }

    [Test]
    public void EnemyFactoryCreatedAttackState()
    {
        var factory = Container.Resolve<EnemyStateFactory>();
        var state = factory.CreateState(StatesEnum.Attack);

        Assert.IsTrue(state is EnemyAttackState);
    }

    [Test]
    public void EnemyFactoryCreatedDeadState()
    {
        var factory = Container.Resolve<EnemyStateFactory>();
        var state = factory.CreateState(StatesEnum.Dead);

        Assert.IsTrue(state is EnemyDeadState);
    }

    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
}