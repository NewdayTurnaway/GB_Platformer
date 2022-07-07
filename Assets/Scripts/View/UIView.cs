using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GB_Platformer
{
    internal sealed class UIView : MonoBehaviour
    {
        [SerializeField] private RectTransform _uiPanelRectTransform;
        [SerializeField] private Text _currentHealthText;
        [SerializeField] private Text _maxHealthText;
        [SerializeField] private Text _currentCoinsText;
        [SerializeField] private List<Image> _itemImages;
        [SerializeField] private RectTransform _messageRectTransform;
        [SerializeField] private Image _messageImage;
        [SerializeField] private Text _messageText;

        public RectTransform UiPanelRectTransform => _uiPanelRectTransform;
        public Text CurrentHealthText => _currentHealthText;
        public Text MaxHealthText => _maxHealthText;
        public Text CurrentCoinsText => _currentCoinsText;
        public List<Image> ItemImages => _itemImages;
        public RectTransform MessageRectTransform => _messageRectTransform;
        public Image MessageImage => _messageImage;
        public Text MessageText => _messageText;
    } 
}
