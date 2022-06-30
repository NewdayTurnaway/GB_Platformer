using UnityEngine;

namespace GB_Platformer
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "GameData/QuestData")]
    public sealed class QuestData : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private QuestType _questType;

        public int Id => _id;
        public QuestType QuestType => _questType;
    } 
}
