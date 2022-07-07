using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    [CreateAssetMenu(fileName = "QuestItemsData", menuName = "GameData/QuestItemsData")]
    public sealed class QuestItemsData : ScriptableObject
    {
        [SerializeField] private List<QuestItemData> _questItemDatas;

        public List<QuestItemData> QuestItemDatas => _questItemDatas;
    } 
}
