using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Mocks
{
    public class MockGameManager : IGameManager
    {
        private IPlayer _player;
        private IEnemy _enemy;

        public MockGameManager(IPlayer player, IEnemy enemy)
        {
            _player = player;
            _enemy = enemy;
        }

        public void LoadStartUpSettingsFile(string fileName)
        {
            
        }

        public void StartGame()
        {
            _player.BeginPlay(MockStats(), new List<Buff>());
            _enemy.BeginPlay(MockStats(), new List<Buff>());
        }

        public void StartGameWithBuffs()
        {
            _player.BeginPlay(MockStats(), MockBuffs());
            _enemy.BeginPlay(MockStats(), MockBuffs());
        }

        /// <summary>
        /// Начать игру с заданными характеристиками
        /// </summary>
        public void StartWithMockStats(float health, float armor, float damage, float vampirism)
        {
            List<Stat> res = new List<Stat>
            {
                //Здоровье
                new Stat()
                {
                    id = 0,
                    value = health
                },
                //Броня
                new Stat()
                {
                    id = 1,
                    value = armor
                },
                //Урон
                new Stat()
                {
                    id = 2,
                    value = damage
                },
                //Вампиризм
                new Stat()
                {
                    id = 3,
                    value = vampirism
                }
            };

            _player.BeginPlay(res, new List<Buff>());
            _enemy.BeginPlay(res, new List<Buff>());
        }

        /// <summary>
        /// Начать игру с заданным баффом
        /// </summary>
        /// <param name="statId">Id характеристики</param>
        /// <param name="value">Значение баффа</param>
        public void StartWithMockBuff(int statId, float value)
        {
            BuffStat[] newBuff = new BuffStat[] { new BuffStat() { statId = statId, value = value } };

            List<Buff> res = new List<Buff>
            {
                new Buff()
                {
                    id = 0,
                    stats = newBuff
                }
            };

            _player.BeginPlay(MockStats(), res);
            _enemy.BeginPlay(MockStats(), res);
        }

        private List<Stat> MockStats()
        {
            List<Stat> res = new List<Stat> 
            {
                //Здоровье
                new Stat()
                {
                    id = 0,
                    value = 100
                },
                //Броня
                new Stat()
                {
                    id = 1,
                    value = 25
                },
                //Урон
                new Stat()
                {
                    id = 2,
                    value = 25
                },
                //Вампиризм
                new Stat()
                {
                    id = 3,
                    value = 0
                }
            };
            
            return res;
        }

        private List<Buff> MockBuffs()
        {
            //Бафф прибавляем 25 к здоровью
            BuffStat[] hpBuff = new BuffStat[1];
            hpBuff[0].statId = 0;
            hpBuff[0].value = 25;

            //Уменьшает броню на 25
            BuffStat[] armorBuff = new BuffStat[1];
            hpBuff[0].statId = 1;
            hpBuff[0].value = -25;

            List<Buff> res = new List<Buff>
            {
                new Buff()
                {
                    id = 0, 
                    stats = hpBuff
                },
                new Buff()
                {
                    id = 1,
                    stats = armorBuff
                }
            };

            return res;
        }
    }
}
