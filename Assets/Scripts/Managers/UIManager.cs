using Assets.Scripts.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        //Ссылки на элементы UI
        [SerializeField]
        private Button _startBuffsButton;

        [SerializeField]
        private Button _startDefaultButton;

        [SerializeField]
        private PlayerPanelHierarchy _PlayerPanel;

        [SerializeField]
        private PlayerPanelHierarchy _EnemyPanel;

        //DI
        [Inject]
        private GameManager _gameManager;

        [Inject]
        private Player _player;

        [Inject]
        private Enemy _enemy;

        private void Awake()
        {
            //Подписка кнопок старта
            _startBuffsButton.onClick.AddListener(() =>
            {
                _gameManager.StartWithBuffs();
            });

            _startDefaultButton.onClick.AddListener(() =>
            {
                _gameManager.StartDefault();
            });

            //Подписка кнопок панелей
            _PlayerPanel.attackButton.onClick.AddListener(() =>
            {
                _player.Attack();
            });

            _EnemyPanel.attackButton.onClick.AddListener(() =>
            {
                _enemy.Attack();
            });
        }
    }
}
