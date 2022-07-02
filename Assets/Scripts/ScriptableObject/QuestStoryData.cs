using UnityEngine;

namespace GB_Platformer
{
    [CreateAssetMenu(fileName = "QuestStoryData", menuName = "GameData/QuestStoryData")]
    public sealed class QuestStoryData : ScriptableObject
    {
        [SerializeField] private QuestData[] _quests;
        [SerializeField] private QuestStoryType _questStoryType;

        public QuestData[] Quests => _quests;
        public QuestStoryType QuestStoryType => _questStoryType;
    } 
}
