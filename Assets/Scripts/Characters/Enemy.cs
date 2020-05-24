﻿using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.States.StateFactories;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Класс противника
    /// </summary>
    public class Enemy : MonoBehaviour, ICharacter
    {
        [SerializeField]
        private Animator _animator;

        /// <summary>
        /// Текущее здоровье
        /// </summary>
        public float HP { get; private set; }

        /// <summary>
        /// Компонент аниматора противника
        /// </summary>
        public Animator EnemyAnimator => _animator;

        public List<Stat> EnemyStats;

        //Приватные поля
        private IState _currentState;
        private EnemyStateFactory _stateFactory;

        //DI
        [Inject]
        private Player _player;

        [Inject]
        public void Construct(EnemyStateFactory stateFactory)
        {
            _stateFactory = stateFactory;

            Debug.Log("Enemy Construct");
        }

        private void Start()
        {
            ChangeState(StatesEnum.Idle);
        }

        private void Update()
        {
            _currentState.OnUpdate();
        }

        /// <summary>
        /// Сменить состояние
        /// </summary>
        /// <param name="state">Новое состояние</param>
        public void ChangeState(StatesEnum state)
        {
            if (_currentState != null)
            {
                _currentState.OnDispose();
                _currentState = null;
            }

            Debug.Log($"Новое состояние: {state}");

            _currentState = _stateFactory.CreateState(state);
            _currentState.OnStart();
        }

        #region ICharacter implementation
        public void Attack()
        {
            ChangeState(StatesEnum.Attack);
        }

        public void TakeDamage(float damage)
        {
            
        }

        public float GetHP()
        {
            return HP;
        }
        #endregion
    }
}
