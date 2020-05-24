using Zenject;
using NUnit.Framework;
using Assets.Scripts.Installers;
using Assets.Scripts.Interface;
using Assets.Scripts.States.StateFactories;

[TestFixture]
public class StateFactoriesUnitTest : ZenjectUnitTestFixture
{
    [SetUp]
    public void BindInterfaces()
    {
        //Запускаем тестовый установщик
        TestInstaller.Install(Container);
    }

    [Test]
    public void PlayerFactoryResolved()
    {
        PlayerStateFactory stateFactory = Container.Resolve<PlayerStateFactory>();

        Assert.NotNull(stateFactory);
    }

    [Test]
    public void EnemyFactoryResolved()
    {
        EnemyStateFactory stateFactory = Container.Resolve<EnemyStateFactory>();

        Assert.NotNull(stateFactory);
    }

    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
}