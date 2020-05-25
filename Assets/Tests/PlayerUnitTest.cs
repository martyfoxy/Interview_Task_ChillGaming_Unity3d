using Zenject;
using NUnit.Framework;
using Assets.Scripts.Installers;
using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
using UnityEngine;
using Assets.Scripts.Mocks;
using Assets.Scripts.States.StateFactories;
using Assets.Scripts.States.PlayerStates;

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

    [Test]
    public void IPlayerResolved()
    {
        IPlayer player = Container.Resolve<IPlayer>();

        Assert.NotNull(player);
    }

    [Test]
    public void PlayerResolved()
    {
        Player player = Container.Resolve<Player>();

        Assert.NotNull(player);
    }

    [Test]
    public void IPlayerResolvedAsPlayer()
    {
        Player player = Container.Resolve<IPlayer>() as Player;

        Assert.NotNull(player);
    }

    [Test]
    public void PlayerAnimattorIsSet()
    {
        Player player = Container.Resolve<Player>();
        Animator playerAnimator = player.GetAnimator();

        Assert.NotNull(playerAnimator);
    }

    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
}