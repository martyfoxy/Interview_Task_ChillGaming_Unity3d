using Zenject;
using NUnit.Framework;
using Assets.Scripts;
using Assets.Scripts.Managers;
using Assets.Scripts.Interface;
using Assets.Scripts.Mocks;
using Assets.Scripts.Installers;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class GameManagerTest : ZenjectUnitTestFixture
{
    [SetUp]
    public void BindInterfaces()
    {
        //Запускаем установщик настроек
        GameSettingsInstaller.InstallFromResource("ScriptableObjects/Installers/GameSettingsInstaller", Container);

        //Mock
        Container.Bind<IPlayer>().To<MockPlayer>().AsSingle();
        Container.Bind<IEnemy>().To<MockEnemy>().AsSingle();
        Container.Bind<IUIManager>().To<MockUIManager>().AsSingle();

        //Биндим игровой менеджер
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
    }

    /// <summary>
    /// Проверяем, что менедер привязан к интерфейсу
    /// </summary>
    [Test]
    public void GameManagerResolve()
    {
        IGameManager manager = Container.Resolve<IGameManager>();

        Assert.NotNull(manager);
    }

    /// <summary>
    /// Проверяем, что нужный класс менеджера привязан к интерфейсу
    /// </summary>
    [Test]
    public void IGameManagerResolvesAsGameManager()
    {
        GameManager manager = Container.Resolve<IGameManager>() as GameManager;

        Assert.NotNull(manager);
    }

    /// <summary>
    /// Проверяем, что игра без баффов началась
    /// </summary>
    [Test]
    public void GameStarted()
    {
        var settings = Container.Resolve<GameSettings>();

        IGameManager manager = Container.Resolve<IGameManager>();

        //Грузим параметры и начинаем игру
        manager.LoadStartUpSettingsFile(GameConst.StartUpSettingsFile);
        manager.StartGame();

        //Проверяем, что у игроков соответствующие настройкам характеристики
        var player = Container.Resolve<IPlayer>();
        Assert.AreEqual(player.GetHP(), settings.Data.stats.First(x => x.title == GameConst.HPName).value);

        var enemy = Container.Resolve<IEnemy>();
        Assert.AreEqual(enemy.GetArmor(), settings.Data.stats.First(x => x.title == GameConst.ArmorName).value);

        //Проверяем, что у игроков нет баффов
        Assert.AreEqual(0, player.GetBuffs().Count);
        Assert.AreEqual(0, enemy.GetBuffs().Count);
    }

    /// <summary>
    /// Проверяем, что игра с баффами началась
    /// </summary>
    [Test]
    public void GameWithBuffsStarted()
    {
        var settings = Container.Resolve<GameSettings>();

        IGameManager manager = Container.Resolve<IGameManager>();

        //Грузим настройки
        manager.LoadStartUpSettingsFile(GameConst.StartUpSettingsFile);

        //Изменяем настройки, чтобы всегда был хотя бы один бафф
        Data fakeData = settings.Data;
        fakeData.settings.buffCountMin = 1;
        settings.SetData(fakeData);

        Assert.AreEqual(1, settings.Data.settings.buffCountMin);

        //начинаем игру
        manager.StartGameWithBuffs();

        //Проверяем, что у игроков соответствующие настройкам характеристики
        var player = Container.Resolve<IPlayer>();
        Assert.AreEqual(player.GetHP(), settings.Data.stats.First(x => x.title == GameConst.HPName).value);

        var enemy = Container.Resolve<IEnemy>();
        Assert.AreEqual(enemy.GetArmor(), settings.Data.stats.First(x => x.title == GameConst.ArmorName).value);

        //Проверяем, что у игроков есть баффы
        Assert.Greater(player.GetBuffs().Count, 0);
        Assert.Greater(enemy.GetBuffs().Count, 0);
    }

    /// <summary>
    /// Проверяем загрузку настроек через игровой менеджер
    /// </summary>
    [Test]
    public void StartUpSettingsFileLoaded()
    {
        //Проверяем, что ссылка на скриптовый объект с настройками привязался
        var settings = Container.Resolve<GameSettings>();
        Assert.NotNull(settings);

        //Записываем туда пустые данные
        settings.SetData(new Data());

        //Грузим настройки через менеджер
        IGameManager manager = Container.Resolve<IGameManager>();
        manager.LoadStartUpSettingsFile(GameConst.StartUpSettingsFile);

        Assert.Greater(settings.Data.stats.Length, 0);
    }


    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
}