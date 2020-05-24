using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.States.StateFactories;
using Boo.Lang;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Класс игрока
    /// </summary>
    public class Player : MonoBehaviour, ICharacter
    {
        [SerializeField]
        private Animator _animator;

        /// <summary>
        /// Текущее здоровье
        /// </summary>
        public float HP { get; private set; }

        /// <summary>
        /// Компонент аниматора игрока
        /// </summary>
        public Animator PlayerAnimator => _animator;

        [Serializable]
        public class PlayerStat
        {
            public float StartHP;
            public float StartArmor;
            public float StartDamage;
            public float StartVampyrism;
        }

        //Приватные поля
        private IState _currentState;
        private PlayerStateFactory _stateFactory;

        //DI
        [Inject]
        private Enemy _enemy;

        [Inject]
        public void Construct(PlayerStateFactory stateFactory)
        {
            _stateFactory = stateFactory;

            Debug.Log("Player Contruct");
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
            if(_currentState != null)
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
