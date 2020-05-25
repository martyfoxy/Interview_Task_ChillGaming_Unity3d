using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.Signals;
using Assets.Scripts.States.StateFactories;
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
        public float HP
        {
            get
            {
                return _currentStats[0];
            }
            private set
            {
                _currentStats[0] = value;
                if (value < 1)
                {
                    _currentStats[0] = 0;
                    ChangeState(StatesEnum.Dead);
                }

                _signalBus.Fire(new CharacterStatChangedSignal() { StatId = 0, Value = _currentStats[0], character = this });
            }
        }

        /// <summary>
        /// Текущая броня
        /// </summary>
        public float Armor
        {
            get
            {
                return _currentStats[1];
            }
            private set
            {
                _currentStats[1] = value;
                _signalBus.Fire(new CharacterStatChangedSignal() { StatId = 1, Value = _currentStats[1], character = this });
            }
        }
        

        /// <summary>
        /// Текущий уроне
        /// </summary>
        public float Damage
        {
            get
            {
                return _currentStats[2];
            }
            private set
            {
                _currentStats[2] = value;
                _signalBus.Fire(new CharacterStatChangedSignal() { StatId = 2, Value = _currentStats[2], character = this });
            }
        }

        /// <summary>
        /// Текущий вампиризм
        /// </summary>
        public float Vampirism
        {
            get
            {
                return _currentStats[3];
            }
            private set
            {
                _currentStats[3] = value;
                _signalBus.Fire(new CharacterStatChangedSignal() { StatId = 3, Value = _currentStats[3], character = this });
            }
        }
        #endregion

        //Словарь текущих характеристик в формате [id - значение]
        private Dictionary<int, float> _currentStats = new Dictionary<int, float>();

        //Приватные поля
        private List<Buff> _buffs;
        private Animator _animator;
        private IState _currentState;

        #region DI private
        private PlayerStateFactory _stateFactory;
        private SignalBus _signalBus;
        #endregion

        [Inject]
        public void Construct(PlayerStateFactory stateFactory, SignalBus signalBus)
        {
            Debug.Log("Player Contruct");

            _stateFactory = stateFactory;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Добавить начальную характеристику
        /// </summary>
        /// <param name="stat"></param>
        private void SetCurrentStat(Stat stat)
        {
            if (!_currentStats.ContainsKey(stat.id))
            {
                _currentStats.Add(stat.id, stat.value);
                _signalBus.Fire(new CharacterStatChangedSignal() { StatId = stat.id, Value = stat.value, character = this });
            }
        }

        #region ICharacter implementation
        public void BeginPlay(List<Stat> startStats, List<Buff> startBuffs)
        {
            //Сброс
            _currentStats = new Dictionary<int, float>();
            _buffs = new List<Buff>(startBuffs);
            _animator.SetBool(GameConst.AttackParameter, false);

            //Заполняем словарь начальными значениями
            startStats.ForEach(x => SetCurrentStat(x));

            _animator.SetInteger(GameConst.HealthParameter, (int)HP);

            //Вычисляем влияние баффов
            for (int i = 0; i < startBuffs.Count; i++)
            {
                Buff buff = startBuffs[i];

                for (int j = 0; j < buff.stats.Length; j++)
                {
                    BuffStat buffStat = buff.stats[j];

                    //Находим стату по id и складываем
                    float oldValue;
                    if(_currentStats.TryGetValue(buffStat.statId, out oldValue))
                    {
                        _currentStats[buffStat.statId] = oldValue + buffStat.value;
                        _signalBus.Fire(new CharacterStatChangedSignal() { StatId = buffStat.statId, Value = _currentStats[buffStat.statId], character = this });
                    }
                }
            }

            //Переход в состояние покоя
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
            var restoredHp = Vampirism / 100 * resDamage;

            HP += restoredHp;
        }

        public void ChangeState(StatesEnum state)
        {
            if (_currentState != null)
                _currentState = null;

            Debug.Log($"{name} новое состояние: {state}");

            _currentState = _stateFactory.CreateState(state);
            _currentState.OnStart();
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

        public List<Buff> GetBuffs()
        {
            return _buffs;
        }
        #endregion
    }
}
