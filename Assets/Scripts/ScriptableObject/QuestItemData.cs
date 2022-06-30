using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    [CreateAssetMenu(fileName = "QuestItemData", menuName = "GameData/QuestItemData")]
    public sealed class QuestItemData : ScriptableObject
    {
        [SerializeField] private int _questId;
        [SerializeField] private List<int> _questItemCollection;

        public int QuestId => _questId;
        public List<int> QuestItemCollection => _questItemCollection;
    } 
}
