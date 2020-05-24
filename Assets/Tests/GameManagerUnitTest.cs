using Zenject;
using NUnit.Framework;
using Assets.Scripts;
using Assets.Scripts.Managers;
using Assets.Scripts.Interface;
using Assets.Scripts.Mocks;
using Assets.Scripts.Installers;

[TestFixture]
public class GameManagerTest : ZenjectUnitTestFixture
{
    [SetUp]
    public void BindInterfaces()
    {
        //Запускаем тестовый установщик
        TestInstaller.Install(Container);
    }

    [Test]
    public void GameManagerResolve()
    {
        IGameManager manager = Container.Resolve<IGameManager>();

        Assert.NotNull(manager);
    }

    [Test]
    public void IGameManagerResolvesAsMockGameManager()
    {
        MockGameManager manager = Container.Resolve<IGameManager>() as MockGameManager;

        Assert.NotNull(manager);
    }

    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
}