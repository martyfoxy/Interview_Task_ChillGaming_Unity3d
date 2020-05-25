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

        public MockGameManager()
        {
            
        }

        public void LoadStartUpSettingsFile(string fileName)
        {
            
        }

        public void StartGame()
        {
            Debug.Log("Начать игру без бафов");
        }

        public void StartGameWithBuffs()
        {
            Debug.Log("Начать игру с бафами");
        }
    }
}
