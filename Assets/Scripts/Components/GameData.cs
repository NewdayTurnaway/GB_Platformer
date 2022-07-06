using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    public sealed class GameData
    {
        [Header("Game Settings")]
        [SerializeField] private Camera _camera;
        [SerializeField] private SpriteAnimationsData _spriteAnimationsData;
        [SerializeField] private UIView _uIView;
        [SerializeField] private ParalaxInfo _paralaxInfo;

        [Header("Level Objects")]
        [SerializeField] private List<CoinView> _coins;
        [SerializeField] private DoorTriggerView _exitDoor;
        [SerializeField] private LevelObjectTrigger _exit;

        [Header("Quests")]
        [SerializeField] private QuestStoryData[] _questStoriesData;
        [SerializeField] private QuestObjectView[] _questObjects;
        [SerializeField] private QuestItemsData _questItemsData;

        [Header("Units Settings")]
        [SerializeField] private PlayerInfo _playerInfo;
        [SerializeField] private CannonInfo _cannonInfo;
        [SerializeField] private EnemiesInfo _enemiesInfo;
        
        public Camera Camera => _camera;
        public SpriteAnimationsData SpriteAnimationsData => _spriteAnimationsData;
        internal UIView UIView => _uIView;
        internal ParalaxInfo Paralax => _paralaxInfo;
        internal List<CoinView> Coins => _coins;
        internal DoorTriggerView ExitDoor => _exitDoor;
        internal LevelObjectTrigger Exit => _exit;
        public QuestStoryData[] QuestStoriesData => _questStoriesData;
        internal QuestObjectView[] QuestObjects => _questObjects;
        public QuestItemsData QuestItemsData => _questItemsData;
        internal PlayerInfo PlayerInfo => _playerInfo;
        internal CannonInfo CannonInfo => _cannonInfo;
        internal EnemiesInfo EnemiesInfo => _enemiesInfo;

        public QuestStoryData GetQuestStory(QuestStoryType type)
        {
            foreach (QuestStoryData questStoryData in _questStoriesData)
            {
                if (questStoryData.QuestStoryType == type)
                {
                    return questStoryData;
                }
            }
            return default;
        }

        public void FindCoinsInScene()
        {
            _coins.Clear();
            _coins = new(Object.FindObjectsOfType<CoinView>());
        }
    }
}