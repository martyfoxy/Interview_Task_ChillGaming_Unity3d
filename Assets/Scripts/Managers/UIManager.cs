using Assets.Scripts.Characters;
using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Signals;
using Assets.Scripts.UI;
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
    public class UIManager : MonoBehaviour, IUIManager
    {
        //Ссылки на элементы UI
        [SerializeField]
        private Button _startBuffsButton;

        [SerializeField]
        private Button _startDefaultButton;

        [SerializeField]
        private PlayerPanelHierarchy _playerPanel;

        [SerializeField]
        private PlayerPanelHierarchy _enemyPanel;

        [SerializeField]
        private Text WinText;

        [SerializeField]
        private GameObject StatPrefab;

        #region DI
        [Inject]
        private IGameManager _gameManager;

        private GameSettings _gameSettings;
        private IPlayer _player;
        private IEnemy _enemy;
        #endregion

        //Словари со ссылками на текст характеристик по ключу - ID характеристики
        private Dictionary<int, Text> _playerStatsTextRefs = new Dictionary<int, Text>();
        private Dictionary<int, Text> _enemyStatsTextRefs = new Dictionary<int, Text>();

        //Кэши спрайтов
        private Dictionary<int, Sprite> _statsSpriteCache = new Dictionary<int, Sprite>();
        private Dictionary<int, Sprite> _buffsSpriteCache = new Dictionary<int, Sprite>();

        [Inject]
        public void Construct(IPlayer player, IEnemy enemy, GameSettings gameSettings)
        {
            Debug.Log("UI Manager Construct");

            _player = player;
            _enemy = enemy;
            _gameSettings = gameSettings;
        }

        private void Awake()
        {
            //Сброс
            _playerStatsTextRefs = new Dictionary<int, Text>();
            _enemyStatsTextRefs = new Dictionary<int, Text>();
            _statsSpriteCache = new Dictionary<int, Sprite>();
            _buffsSpriteCache = new Dictionary<int, Sprite>();

            //Подписка кнопок старта
            _startBuffsButton.onClick.AddListener(() =>
            {
                _gameManager.StartGameWithBuffs();
            });

            _startDefaultButton.onClick.AddListener(() =>
            {
                _gameManager.StartGame();
            });

            //Подписка кнопок панелей
            _playerPanel.attackButton.onClick.AddListener(() =>
            {
                _player.Attack();
            });

            _enemyPanel.attackButton.onClick.AddListener(() =>
            {
                _enemy.Attack();
            });
        }

        private void Update()
        {
            //Отключаем кнопки атаки, когда у аниматора состояние атаки или смерти
            _playerPanel.attackButton.interactable = _playerPanel.character.GetCurrentAnimatorStateInfo(0).IsName(GameConst.IdleAnimationName);
            _enemyPanel.attackButton.interactable = _enemyPanel.character.GetCurrentAnimatorStateInfo(0).IsName(GameConst.IdleAnimationName);
        }

        #region IUIManager implementation
        public void Reset()
        {
            _playerStatsTextRefs = new Dictionary<int, Text>();
            _enemyStatsTextRefs = new Dictionary<int, Text>();
            WinText.gameObject.SetActive(false);

            foreach (Transform child in _playerPanel.statsPanel)
                Destroy(child.gameObject);

            foreach (Transform child in _enemyPanel.statsPanel)
                Destroy(child.gameObject);
        }

        public void ShowStatsIcons()
        {
            //Получаем список характеристик, которые есть у каждого игрока
            List<Stat> stats = _gameSettings.Data.stats.ToList();

            //Создаем иконки на панелях и запоминаем ссылки на них
            stats.ForEach(stat =>
            {
                AddStatToPanel(_playerPanel.statsPanel, stat, true);
                AddStatToPanel(_enemyPanel.statsPanel, stat, false);
            });
        }

        public void ShowBuffsIcons()
        {
            _player.GetBuffs().ForEach(buff =>
            {
                AddBuffToPanel(_playerPanel.statsPanel, buff);
            });

            _enemy.GetBuffs().ForEach(buff =>
            {
                AddBuffToPanel(_enemyPanel.statsPanel, buff);
            });
        }

        public void UpdateValue(CharacterStatChangedSignal signal)
        {
            if (signal.character.GetType() == _player.GetType())
                _playerStatsTextRefs[signal.StatId].text = signal.Value.ToString();
            else
                _enemyStatsTextRefs[signal.StatId].text = signal.Value.ToString(); ;
        }

        public void EndOfGame(EndOfGameSignal signal)
        {
            WinText.gameObject.SetActive(true);
            if (signal.looser.GetType() == _player.GetType())
                WinText.text = "Enemy победил!";
            else
                WinText.text = "Player победил!";
        }
        #endregion

        /// <summary>
        /// Добавить иконку характеристики на панель
        /// </summary>
        /// <param name="panel">Панель</param>
        /// <param name="stat">Стата</param>
        /// <param name="isPlayer">Флаг, являются ли игроком или противником</param>
        private void AddStatToPanel(Transform panel, Stat stat, bool isPlayer)
        {
            Sprite sprite;
            if(!_statsSpriteCache.TryGetValue(stat.id, out sprite))
            {
                //Грузим спрайт и сохраняем в кэш
                sprite = Resources.Load<Sprite>("Icons/" + stat.icon);
                _statsSpriteCache.Add(stat.id, sprite);
            }

            //Создаем иконки
            var go = InstantiateIconOnPanel(panel, sprite);

            //Кэшируем ссылки на текст
            var textComp = go.GetComponentInChildren<Text>();
            if (isPlayer)
            {
                if (!_playerStatsTextRefs.ContainsKey(stat.id))
                    _playerStatsTextRefs.Add(stat.id, textComp);
            }
            else
            {
                if (!_enemyStatsTextRefs.ContainsKey(stat.id))
                    _enemyStatsTextRefs.Add(stat.id, textComp);
            }
        }

        /// <summary>
        /// Добавить иконку баффа на панель
        /// </summary>
        /// <param name="panel">Панель</param>
        /// <param name="buff">Бафф</param>
        private void AddBuffToPanel(Transform panel, Buff buff)
        {
            Sprite sprite;
            if(!_buffsSpriteCache.TryGetValue(buff.id, out sprite))
            {
                //Грузим спрайт и сохраняем в кэш
                sprite = Resources.Load<Sprite>("Icons/" + buff.icon);
                _buffsSpriteCache.Add(buff.id, sprite);
            }
            
            //Создаем иконки и отображаем имя баффа
            var go = InstantiateIconOnPanel(panel, sprite);
            go.GetComponentInChildren<Text>().text = buff.title;
        }

        /// <summary>
        /// Создать префаб с иконкой на панели
        /// </summary>
        /// <param name="panel">Трансформ панели</param>
        /// <param name="icon">Спрайт с иконкой</param>
        private GameObject InstantiateIconOnPanel(Transform panel, Sprite icon)
        {
            var go = Instantiate(StatPrefab, panel);

            var iconComp = go.GetComponentsInChildren<Image>().First(x => x.name == GameConst.IconName);
            iconComp.sprite = icon;

            return go;
        }
    }
}
