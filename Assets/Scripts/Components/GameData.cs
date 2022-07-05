using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class GameData
    {
        [Header("Game Settings")]
        [SerializeField] private Camera _camera;
        [SerializeField] private SpriteAnimationsData _spriteAnimationsData;
        [SerializeField] private UIView _uIView;
        [SerializeField] private ParalaxInfo _paralaxInfo;

        [Header("Level Objects")]
        [SerializeField] private List<LevelObjectView> _coins;
        [SerializeField] private List<LevelObjectView> _elevators;
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
        public UIView UIView => _uIView;
        public ParalaxInfo Paralax => _paralaxInfo;
        public List<LevelObjectView> Coins => _coins;
        public List<LevelObjectView> Elevators => _elevators;
        public DoorTriggerView ExitDoor => _exitDoor;
        public LevelObjectTrigger Exit => _exit;
        public QuestStoryData[] QuestStoriesData => _questStoriesData;
        public QuestObjectView[] QuestObjects => _questObjects;
        public QuestItemsData QuestItemsData => _questItemsData;
        public PlayerInfo PlayerInfo => _playerInfo;
        public CannonInfo CannonInfo => _cannonInfo;
        public EnemiesInfo EnemiesInfo => _enemiesInfo;

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
    }
}