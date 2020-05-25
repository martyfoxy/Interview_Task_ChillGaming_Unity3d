using Zenject;
using NUnit.Framework;
using Assets.Scripts.Characters;
using Assets.Scripts.Installers;
using Assets.Scripts.Interface;
using UnityEngine;
using Assets.Scripts.Mocks;

[TestFixture]
public class EnemyUnitTest : ZenjectUnitTestFixture
{
    [SetUp]
    public void BindInterfaces()
    {
        //Запускаем тестовый установщик
        TestInstaller.Install(Container);

        Container.BindInterfacesAndSelfTo<Enemy>().FromNewComponentOnNewPrefabResource("Characters/Character_model").AsSingle();

        Container.BindInterfacesAndSelfTo<MockPlayer>().AsSingle();
    }

    [Test]
    public void IEnemyResolved()
    {
        IEnemy enemy = Container.Resolve<IEnemy>();

        Assert.NotNull(enemy);
    }

    [Test]
    public void EnemyResolved()
    {
        Enemy enemy = Container.Resolve<Enemy>();

        Assert.NotNull(enemy);
    }

    [Test]
    public void IEnemyResolvedAsEnemy()
    {
        Enemy enemy = Container.Resolve<IEnemy>() as Enemy;

        Assert.NotNull(enemy);
    }

    [Test]
    public void EnemyAnimatorIsSet()
    {
        Enemy enemy = Container.Resolve<Enemy>();
        Animator enemyAnimator = enemy.GetAnimator();

        Assert.NotNull(enemyAnimator);
    }

    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
}