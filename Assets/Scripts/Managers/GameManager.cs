using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Игровой менеджер
    /// </summary>
    public class GameManager : IInitializable, IGameManager
    {
        //DI
        private IPlayer _player;
        private IEnemy _enemy;
        private GameSettings _gameSettings;

        [Inject]
        private UIManager _uiManager;

        public GameManager(IPlayer player, IEnemy enemy, GameSettings gameSettings)
        {
            _player = player;
            _enemy = enemy;
            _gameSettings = gameSettings;
        }

        public void Initialize()
        {
            Debug.Log("GameManager init");

            //Десериализуем json с данными игры и засовываем в скриптовый объект
            TextAsset dataFile = Resources.Load("data") as TextAsset;
            _gameSettings.data = JsonUtility.FromJson<Data>(dataFile.text);

            //По-умолчанию начинаем игру без бафов
            StartDefault();
        }

        #region IGameManager implementation
        public void StartDefault()
        {
            Debug.Log("Начать игру без бафов");

            _uiManager.Reset();
            
            var startStats = _gameSettings.data.stats.ToList();
            _player.SetDefault(startStats, new List<Buff>());
            _enemy.SetDefault(startStats, new List<Buff>());

            _uiManager.ShowStatsIcons();
        }

        public void StartWithBuffs()
        {
            Debug.Log("Начать игру с бафами");

            _uiManager.Reset();

            var startStats = _gameSettings.data.stats.ToList();
            _player.SetDefault(startStats, GetRandomBuffs());
            _enemy.SetDefault(startStats, GetRandomBuffs());

            _uiManager.ShowStatsIcons();
            _uiManager.ShowBuffsIcons();
        }
        #endregion

        /// <summary>
        /// Получить список случайных бафов, согласно настройкам
        /// </summary>
        /// <returns>Список бафов</returns>
        private List<Buff> GetRandomBuffs()
        {
            List<Buff> buffList = new List<Buff>();

            //Случайное количество бафов
            var buffCount = Random.Range(_gameSettings.data.settings.buffCountMin, _gameSettings.data.settings.buffCountMax);

            for (int i = 0; i < buffCount; i++)
            {
                var randomIndex = Random.Range(0, _gameSettings.data.buffs.Length - 1);
                var newBuff = _gameSettings.data.buffs[randomIndex];

                if (_gameSettings.data.settings.allowDuplicateBuffs || !buffList.Contains(newBuff))
                    buffList.Add(newBuff);
            }

            return buffList;
        }
    }
}
