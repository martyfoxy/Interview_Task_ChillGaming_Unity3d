using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.ScriptableObjects;
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
    /// <summary>
    /// Менеджер UI
    /// </summary>
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

        [SerializeField]
        private GameObject StatPrefab;

        [Inject]
        public void Construct(IGameManager gameManager, IPlayer player, IEnemy enemy, GameSettings gameSettings)
        {
            Debug.Log("UI Manager Construct");

            _gameManager = gameManager;
            _player = player;
            _enemy = enemy;
            _gameSettings = gameSettings;
        }

        //DI
        private IGameManager _gameManager;
        private GameSettings _gameSettings;
        private IPlayer _player;
        private IEnemy _enemy;

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

        private void Update()
        {
            //Отключаем кнопки атаки, когда у аниматора состояние атаки или смерти
            _PlayerPanel.attackButton.interactable = _PlayerPanel.character.GetCurrentAnimatorStateInfo(0).IsName(AnimationNamesConst.IdleAnimationName);
            _EnemyPanel.attackButton.interactable = _EnemyPanel.character.GetCurrentAnimatorStateInfo(0).IsName(AnimationNamesConst.IdleAnimationName);
        }

        /// <summary>
        /// Очистить иконки на панелях
        /// </summary>
        public void Reset()
        {
            foreach(Transform child in _PlayerPanel.statsPanel)
                Destroy(child.gameObject);
            foreach (Transform child in _EnemyPanel.statsPanel)
                Destroy(child.gameObject);
        }

        /// <summary>
        /// Отобразить иконки статы
        /// </summary>
        public void ShowStatsIcons()
        {
            _player.GetStats().ForEach(stat =>
            {
                AddStatToPanel(_PlayerPanel.statsPanel, stat);
            });

            _enemy.GetStats().ForEach(stat =>
            {
                AddStatToPanel(_EnemyPanel.statsPanel, stat);
            });
        }

        /// <summary>
        /// Отобразить иконки баффов
        /// </summary>
        public void ShowBuffsIcons()
        {
            _player.GetBuffs().ForEach(buff =>
            {
                AddBuffToPanel(_PlayerPanel.statsPanel, buff);
            });

            _enemy.GetBuffs().ForEach(buff =>
            {
                AddBuffToPanel(_EnemyPanel.statsPanel, buff);
            });
        }

        /// <summary>
        /// Добавить иконку статы на панель
        /// </summary>
        /// <param name="panel">Панель</param>
        /// <param name="stat">Стата</param>
        private void AddStatToPanel(Transform panel, Stat stat)
        {
            var icon = Resources.Load<Sprite>("Icons/" + stat.icon);
            AddIconToPanel(panel, icon, stat.value.ToString());
        }

        /// <summary>
        /// Добавить иконку баффа на панель
        /// </summary>
        /// <param name="panel">Панель</param>
        /// <param name="buff">Бафф</param>
        private void AddBuffToPanel(Transform panel, Buff buff)
        {
            var icon = Resources.Load<Sprite>("Icons/" + buff.icon);
            AddIconToPanel(panel, icon, buff.title);
        }

        /// <summary>
        /// Добавить иконку на панель
        /// </summary>
        /// <param name="panel">Панель</param>
        /// <param name="icon">Иконка</param>
        private void AddIconToPanel(Transform panel, Sprite icon, string title)
        {
            var go = Instantiate(StatPrefab, panel);
            var iconComp = go.GetComponentsInChildren<Image>().First(x => x.name == HierarchyNamesConst.IconName);
            var textComp = go.GetComponentInChildren<Text>();
            iconComp.sprite = icon;
            textComp.text = title;
        }
    }
}
