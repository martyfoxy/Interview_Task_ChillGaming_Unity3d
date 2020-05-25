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
        GameSettingsInstaller.InstallFromResource("ScriptableObjects/Installers/GameSettingsInstaller", Container);

        //Запускаем тестовый установщик
        TestInstaller.Install(Container);

        Container.Bind<IPlayer>().To<MockPlayer>().AsSingle();
        Container.Bind<IEnemy>().To<MockEnemy>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
    }

    [Test]
    public void GameManagerResolve()
    {
        IGameManager manager = Container.Resolve<IGameManager>();

        Assert.NotNull(manager);
    }

    [Test]
    public void IGameManagerResolvesAsGameManager()
    {
        GameManager manager = Container.Resolve<IGameManager>() as GameManager;

        Assert.NotNull(manager);
    }

    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
}