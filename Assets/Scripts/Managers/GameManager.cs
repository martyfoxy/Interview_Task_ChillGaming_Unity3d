using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Enums;
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
        #region DI
        [Inject]
        private IUIManager _uiManager;

        private IPlayer _player;
        private IEnemy _enemy;
        private GameSettings _gameSettings;
        #endregion

        public GameManager(IPlayer player, IEnemy enemy, GameSettings gameSettings)
        {
            Debug.Log("GameManager Constructor");

            _player = player;
            _enemy = enemy;
            _gameSettings = gameSettings;
        }

        public void Initialize()
        {
            Debug.Log("GameManager Init");

            //Грузим настройки из текстового файла в скриетовый объект
            LoadStartUpSettingsFile(GameConst.StartUpSettingsFile);

            //По-умолчанию начинаем игру без бафов
            StartGame();
        }

        #region IGameManager implementation
        public void StartGame()
        {
            Debug.Log("Начать игру без бафов");

            _uiManager.Reset();
            _uiManager.ShowStatsIcons();

            var startStats = _gameSettings.Data.stats.ToList();
            _player.BeginPlay(startStats, new List<Buff>());
            _enemy.BeginPlay(startStats, new List<Buff>());
        }

        public void StartGameWithBuffs()
        {
            Debug.Log("Начать игру с бафами");

            _uiManager.Reset();
            _uiManager.ShowStatsIcons();

            var startStats = _gameSettings.Data.stats.ToList();
            _player.BeginPlay(startStats, GetRandomBuffs());
            _enemy.BeginPlay(startStats, GetRandomBuffs());
            
            _uiManager.ShowBuffsIcons();
        }

        public void LoadStartUpSettingsFile(string fileName)
        {
            //Сброс
            _gameSettings.SetData(new Data());

            TextAsset asset = Resources.Load(fileName) as TextAsset;

            if (asset == null)
                throw new Exception("Не удалось загрузить ассет с настройками");

            _gameSettings.SetData(JsonUtility.FromJson<Data>(asset.text));
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
            var buffCount = Random.Range(_gameSettings.Data.settings.buffCountMin, _gameSettings.Data.settings.buffCountMax);

            for (int i = 0; i < buffCount; i++)
            {
                var randomIndex = Random.Range(0, _gameSettings.Data.buffs.Length - 1);
                var newBuff = _gameSettings.Data.buffs[randomIndex];

                if (_gameSettings.Data.settings.allowDuplicateBuffs || !buffList.Contains(newBuff))
                    buffList.Add(newBuff);
            }

            return buffList;
        }
    }
}
