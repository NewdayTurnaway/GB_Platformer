using UnityEngine;

namespace GB_Platformer
{
    [CreateAssetMenu(fileName = "QuestItemData", menuName = "GameData/QuestItemData")]
    public sealed class QuestItemData : ScriptableObject
    {
        [SerializeField] private int _questId;
        [SerializeField] private Sprite _sprite;
        [TextArea]
        [SerializeField] private string _itemDescription;

        public int QuestId => _questId;
        public Sprite Sprite => _sprite;
        public string ItemDescription => _itemDescription;
    } 
}
