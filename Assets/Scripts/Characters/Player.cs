using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.States.StateFactories;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Класс игрока
    /// </summary>
    public class Player : MonoBehaviour, IPlayer
    {
        #region Properties
        /// <summary>
        /// Текущее здоровье
        /// </summary>
        public float HP;// { get; private set; }

        /// <summary>
        /// Текущая броня
        /// </summary>
        public float Armor;// { get; private set; }

        /// <summary>
        /// Текущий уроне
        /// </summary>
        public float Damage;// { get; private set; }

        /// <summary>
        /// Текущий вампиризм
        /// </summary>
        public float Vampirism;// { get; private set; }
        #endregion


        public List<Stat> DefaultStats;
        public List<Buff> PlayerBuffs;


        //Приватные поля
        private Animator _animator;
        private IState _currentState;

        //DI
        private PlayerStateFactory _stateFactory;
        private IEnemy _enemy;

        [Inject]
        public void Construct(PlayerStateFactory stateFactory, IEnemy enemy)
        {
            Debug.Log("Player Contruct");

            _stateFactory = stateFactory;
            _enemy = enemy;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            //ChangeState(StatesEnum.Idle);
        }

        private void Update()
        {
            _currentState.OnUpdate();
        }

        #region ICharacter implementation
        public void SetDefault(List<Stat> startStats, List<Buff> startBuffs)
        {
            DefaultStats = new List<Stat>(startStats);
            PlayerBuffs = new List<Buff>(startBuffs);

            //Вычисляем влияние баффов
            for (int i = 0; i < startBuffs.Count; i++)
            {
                Buff buff = startBuffs[i];

                for (int j = 0; j < buff.stats.Length; j++)
                {
                    BuffStat buffStat = buff.stats[j];

                    //Находим параметр по id и складываем значения
                    Stat stat = DefaultStats.Find(x => x.id == buffStat.statId);
                    if(stat != null)
                        stat.value += buffStat.value;
                }
            }

            //Применяем параметры
            HP = DefaultStats[0].value;
            Armor = DefaultStats[1].value;
            Damage = DefaultStats[2].value;
            Vampirism = DefaultStats[3].value;

            //Сброс
            _animator.SetInteger(AnimationParametersConst.HealthParameter, (int)HP);
            _animator.SetBool(AnimationParametersConst.AttackParameter, false);
            ChangeState(StatesEnum.Idle);
        }

        public void Attack()
        {
            _currentState.Attack();
        }

        public float TakeDamage(float damage)
        {
            //Урон, гасящийся броней
            var resDamage = (100 - Armor) / 100 * damage;

            //Уменьшаем hp
            HP -= resDamage;

            return resDamage;
        }

        public void VampirismRestore(float resDamage)
        {
            //В зависимости от нанесенного урона, вычисляем восстановленное hp, зависящее от вампиризма
            var restoredHp = (100 - Vampirism) / 100 * resDamage;

            HP += restoredHp;
        }

        public float GetHP()
        {
            return HP;
        }

        public float GetArmor()
        {
            return Armor;
        }

        public float GetDamage()
        {
            return Damage;
        }

        public float GetVampirism()
        {
            return Vampirism;
        }

        public Animator GetAnimator()
        {
            return _animator;
        }

        public void ChangeState(StatesEnum state)
        {
            if (_currentState != null)
            {
                _currentState.OnDispose();
                _currentState = null;
            }

            Debug.Log($"{name} новое состояние: {state}");

            _currentState = _stateFactory.CreateState(state);
            _currentState.OnStart();
        }

        public List<Stat> GetStats()
        {
            return DefaultStats;
        }

        public List<Buff> GetBuffs()
        {
            return PlayerBuffs;
        }
        #endregion
    }
}
