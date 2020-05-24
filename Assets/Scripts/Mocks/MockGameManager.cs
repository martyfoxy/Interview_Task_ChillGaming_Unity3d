using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
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
        private Player _player;

        public MockGameManager()
        {
            _player = new Player();
        }

        public void StartDefault()
        {
            Debug.Log("Начать игру без бафов");
        }

        public void StartWithBuffs()
        {
            Debug.Log("Начать игру с бафами");
        }
    }
}
