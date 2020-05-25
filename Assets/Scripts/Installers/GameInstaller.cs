using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Signals;
using Assets.Scripts.States;
using Assets.Scripts.States.EnemyStates;
using Assets.Scripts.States.PlayerStates;
using Assets.Scripts.States.StateFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
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

            Container.DeclareSignal<CharacterStatChangedSignal>();
            Container.BindSignal<CharacterStatChangedSignal>().ToMethod<UIManager>(x => x.UpdateValue).FromResolve();

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
