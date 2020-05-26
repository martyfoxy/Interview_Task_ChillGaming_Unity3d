using Zenject;
using NUnit.Framework;
using Assets.Scripts.Installers;
using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
using UnityEngine;
using Assets.Scripts.Mocks;
using Assets.Scripts.States.PlayerStates;
using Assets.Scripts.Signals;
using Assets.Scripts.Models.Enums;

[TestFixture]
public class PlayerUnitTest : ZenjectUnitTestFixture
{
    [SetUp]
    public void BindInterfaces()
    {
        //Запускаем тестовый установщик
        TestInstaller.Install(Container);

        Container.BindInterfacesAndSelfTo<Player>().FromNewComponentOnNewPrefabResource("Characters/Character_model").AsSingle();
        Container.BindInterfacesAndSelfTo<MockEnemy>().AsSingle();
    }

    /// <summary>
    /// Проверяем, что игрок привязан к интерфейсу
    /// </summary>
    [Test]
    public void IPlayerResolved()
    {
        IPlayer player = Container.Resolve<IPlayer>();

        Assert.NotNull(player);
    }

    /// <summary>
    /// Проверяем, что нужный класс игрока привязан к интерфейсу
    /// </summary>
    [Test]
    public void PlayerResolved()
    {
        Player player = Container.Resolve<Player>();

        Assert.NotNull(player);
    }

    /// <summary>
    /// Проверяем, что у игрока есть аниматор
    /// </summary>
    [Test]
    public void PlayerAnimattorIsSet()
    {
        IPlayer player = Container.Resolve<IPlayer>();
        Animator playerAnimator = player.GetAnimator();

        Assert.NotNull(playerAnimator);
    }

    /// <summary>
    /// Проверяем, что игрок переходит между состояниями
    /// </summary>
    [Test]
    public void PlayerChangedState()
    {
        Player player = Container.Resolve<Player>();

        player.ChangeState(StatesEnum.Idle);
        Assert.IsTrue(player.CurrentState is PlayerIdleState);
        Assert.IsAssignableFrom(typeof(PlayerIdleState), player.CurrentState);

        player.ChangeState(StatesEnum.Dead);
        Assert.IsAssignableFrom(typeof(PlayerDeadState), player.CurrentState);
    }

    /// <summary>
    /// Проверяем, что игрок правильно вычисляет результирующей урон после нанесенной атаки
    /// </summary>
    /// <param name="enemyDamage">Нанесенный урон</param>
    /// <param name="playerArmor">Броня игрока</param>
    /// <param name="expected">Результирующий урон</param>
    [TestCase(25f, 100f, 0f)]
    [TestCase(10f, 25f, 7.5f)]
    [TestCase(20f, 50f, 10f)]
    [TestCase(0f, 10f, 0f)]
    [TestCase(25f, 0f, 25f)]
    [TestCase(20f, -10f, 22f)]
    public void PlayerTakeDamageResultDamage(float enemyDamage, float playerArmor, float expected)
    {
        Player player = Container.Resolve<Player>();

        //Создаем фейковый игровой менеджер
        Container.BindInterfacesAndSelfTo<MockGameManager>().AsSingle();

        var gameManager = Container.Resolve<MockGameManager>();
        gameManager.StartWithMockStats(100, playerArmor, 0, 0);

        //Наносим урон игроку
        var res = player.TakeDamage(enemyDamage);

        Assert.AreEqual(res, expected);
    }

    /// <summary>
    /// Проверяем, что игрок правильно вычисляет здоровье после нанесенного урона
    /// </summary>
    /// <param name="enemyDamage">Нанесенный урон</param>
    /// <param name="playerArmor">Броня игрока</param>
    /// <param name="expected">Результирующее здоровье</param>
    [TestCase(25f, 100f, 100f)]
    [TestCase(10f, 25f, 92.5f)]
    [TestCase(20f, 50f, 90f)]
    [TestCase(0f, 10f, 100f)]
    [TestCase(25f, 0f, 75f)]
    [TestCase(20f, -10f, 78f)]
    public void PlayerTakeDamageResultHealth(float enemyDamage, float playerArmor, float expected)
    {
        Player player = Container.Resolve<Player>();

        //Создаем фейковый игровой менеджер
        Container.BindInterfacesAndSelfTo<MockGameManager>().AsSingle();

        var gameManager = Container.Resolve<MockGameManager>();
        gameManager.StartWithMockStats(100, playerArmor, 0, 0);

        //Наносим урон игроку
        player.TakeDamage(enemyDamage);

        Assert.AreEqual(player.HP, expected);
    }

    /// <summary>
    /// Проверяем, что вампиризм восстанавливает нужно количество здоровья
    /// </summary>
    /// <param name="playerVampirism">Вампиризм игрока</param>
    /// <param name="enemyDamage">Нанесенный урон</param>
    /// <param name="expected">Результирующее здоровье</param>
    [TestCase(50, 50, 125)]
    [TestCase(50, 100, 150)]
    [TestCase(25, 100, 125)]
    [TestCase(25, 100, 125)]
    [TestCase(25, -100, 100)]
    [TestCase(25, 0, 100)]
    public void PlayerVampirismRestore(float playerVampirism, float enemyDamage, float expected)
    {
        Player player = Container.Resolve<Player>();

        //Создаем фейковый игровой менеджер
        Container.BindInterfacesAndSelfTo<MockGameManager>().AsSingle();

        var gameManager = Container.Resolve<MockGameManager>();
        gameManager.StartWithMockStats(100, 0, 0, playerVampirism);

        player.VampirismRestore(enemyDamage);

        Assert.AreEqual(player.HP, expected);
    }

    /// <summary>
    /// Проверяем, что игрок переходит в состояние смерти, когда его здоровье меньше 1
    /// </summary>
    /// <param name="health">Начальное здоровье</param>
    /// <param name="damage">Уровень нанесенного урона</param>
    /// <param name="isAlive">Жив ли игрок</param>
    [TestCase(100, 0, true)]
    [TestCase(50, 40, true)]
    [TestCase(1, 0, true)]
    [TestCase(20, 20, false)]
    [TestCase(0, 0, false)]
    [TestCase(-10, 0, false)]
    public void PlayerDeadWhenHealth(float health, float damage, bool isAlive)
    {
        Player player = Container.Resolve<Player>();

        //Создаем фейковый игровой менеджер
        Container.BindInterfacesAndSelfTo<MockGameManager>().AsSingle();
        var gameManager = Container.Resolve<MockGameManager>();
        gameManager.StartWithMockStats(health, 0, 0, 0);

        player.TakeDamage(damage);

        Assert.AreEqual(player.CurrentState is PlayerIdleState, isAlive);
    }

    /// <summary>
    /// Проверяем, что при атаке игрок наносит урон
    /// </summary>
    [Test]
    public void PlayerAttackState()
    {
        Player player = Container.Resolve<Player>();

        //Создаем фейковый игровой менеджер
        Container.BindInterfacesAndSelfTo<MockGameManager>().AsSingle();
        var gameManager = Container.Resolve<MockGameManager>();
        gameManager.StartGame();

        float oldHp = Container.Resolve<MockEnemy>().GetHP();

        //Атакуем
        player.Attack();

        float newHp = Container.Resolve<MockEnemy>().GetHP();

        Assert.AreNotEqual(oldHp, newHp);
    }

    /// <summary>
    /// Проверяем, что значения здоровье вычислилось правильно при баффе
    /// </summary>
    [TestCase(25, 125)]
    [TestCase(-25, 75)]
    public void PlayerHealthBuffCalculatedCorrectly(float addHealth, float expected)
    {
        Player player = Container.Resolve<Player>();

        //Создаем фейковый игровой менеджер
        Container.BindInterfacesAndSelfTo<MockGameManager>().AsSingle();
        var gameManager = Container.Resolve<MockGameManager>();

        gameManager.StartWithMockBuff(0, addHealth);

        Assert.AreEqual(player.HP, expected);
    }

    /// <summary>
    /// Проверяем, что значения брони вычислилось правильно при баффе
    /// </summary>
    [TestCase(25, 50)]
    [TestCase(-25, 0)]
    public void PlayerArmorBuffCalculatedCorrectly(float addArmor, float expected)
    {
        Player player = Container.Resolve<Player>();

        //Создаем фейковый игровой менеджер
        Container.BindInterfacesAndSelfTo<MockGameManager>().AsSingle();
        var gameManager = Container.Resolve<MockGameManager>();

        gameManager.StartWithMockBuff(1, addArmor);

        Assert.AreEqual(player.Armor, expected);
    }

    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
}