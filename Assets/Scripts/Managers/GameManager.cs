using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.ScriptableObjects;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Игровой менеджер
    /// </summary>
    public class GameManager : IInitializable, IGameManager
    {
        private Data _gameData;
        private Player _player;
        private Enemy _enemy;

        public GameManager(Player player, Enemy enemy)
        {
            _player = player;
            _enemy = enemy;
        }

        public void Initialize()
        {
            Debug.Log("GameManager init");

            //Десериализуем данные игры
            TextAsset dataFile = Resources.Load("data") as TextAsset;
            _gameData = JsonUtility.FromJson<Data>(dataFile.text);

        }

        #region IGameManager implementation
        public void StartDefault()
        {
            Debug.Log("Начать игру без бафов");
        }

        public void StartWithBuffs()
        {
            Debug.Log("Начать игру с бафами");
        }
        #endregion
    }
}
