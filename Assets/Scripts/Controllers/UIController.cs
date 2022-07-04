using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GB_Platformer
{
    internal sealed class UIController : IInitialization, IExecute, IDeinitialization
    {
        private readonly PlayerView _playerView;
        private readonly RectTransform _uiPanelRectTransform;
        private readonly Text _currentHealthText;
        private readonly Text _maxHealthText;
        private readonly Text _currentCoinsText;
        private readonly Text _maxCoinsText;
        private readonly List<Image> _itemImages;
        private readonly RectTransform _messageRectTransform;
        private readonly Image _messageImage;
        private readonly Text _messageText;
        private readonly List<QuestItemData> _questItemDatas;
        private readonly QuestObjectView[] _questObjects;

        private float _messageTimer;
        private bool _ShowMessage;
        private int _itemsCount;

        public UIController(UIView uIView, PlayerView playerView, QuestItemsData questItemsData, QuestObjectView[] questObjects, float maxCoins)
        {
            _playerView = playerView;
            _uiPanelRectTransform = uIView.UiPanelRectTransform;
            _currentHealthText = uIView.CurrentHealthText;
            _maxHealthText = uIView.MaxHealthText;
            _currentCoinsText = uIView.CurrentCoinsText;
            _maxCoinsText = uIView.MaxCoinsText;
            _itemImages = uIView.ItemImages;
            _messageRectTransform = uIView.MessageRectTransform;
            _messageImage = uIView.MessageImage;
            _messageText= uIView.MessageText;
            _questItemDatas = questItemsData.QuestItemDatas;
            _questObjects = questObjects;

            _maxHealthText.text = _playerView.Health.MaxHealth.ToString();
            _currentHealthText.text = _playerView.Health.CurrentHealth.ToString();
            _maxCoinsText.text = maxCoins.ToString();
            _currentCoinsText.text = _playerView.CoinsCounter.ToString();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_uiPanelRectTransform);
        }

        public void Initialization()
        {
            _playerView.ChangeHeath += ChangeHealth;
            _playerView.ChangeCoinsCounter += ChangeCoinsCounter;

            for (int i = 0; i < _itemImages.Count; i++)
            {
                _itemImages[i].gameObject.SetActive(false);
            }
            
            _messageRectTransform.gameObject.SetActive(false);

            for (int i = 0; i < _questObjects.Length; i++)
            {
                _questObjects[i].QuestItem += ApplyItem;
            }
        }

        public void Execute()
        {
            _messageRectTransform.gameObject.SetActive(_ShowMessage);
            if (!_ShowMessage)
            {
                return;
            }

            if (_messageTimer > 0)
            {
                _messageTimer -= Time.deltaTime;
                
            }
            else
            {
                _messageTimer = 0;
                _ShowMessage = false;
            }
        }

        public void Deinitialization()
        {
            _playerView.ChangeHeath -= ChangeHealth;
            _playerView.ChangeCoinsCounter -= ChangeCoinsCounter;

            for (int i = 0; i < _questObjects.Length; i++)
            {
                _questObjects[i].QuestItem -= ApplyItem;
            }

            _itemImages.Clear();
        }

        private void ChangeHealth()
        {
            _currentHealthText.text = _playerView.Health.CurrentHealth.ToString();
            _maxHealthText.text = _playerView.Health.MaxHealth.ToString();
        }

        private void ChangeCoinsCounter()
        {
            _currentCoinsText.text = _playerView.CoinsCounter.ToString();
        }

        private void ApplyItem(int questItemId)
        {
            if (_itemsCount == _itemImages.Count)
            {
                return;
            }

            for (int i = 0; i < _questItemDatas.Count; i++)
            {
                if(questItemId == _questItemDatas[i].QuestId)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(_uiPanelRectTransform);
                    _itemImages[_itemsCount].gameObject.SetActive(true);
                    _itemImages[_itemsCount].sprite = _questItemDatas[i].Sprite;
                    _itemsCount++;

                    _ShowMessage = true;
                    _messageRectTransform.gameObject.SetActive(_ShowMessage);
                    _messageImage.sprite = _questItemDatas[i].Sprite;
                    _messageText.text = _questItemDatas[i].ItemDescription;
                    LayoutRebuilder.ForceRebuildLayoutImmediate(_messageRectTransform);
                    _messageTimer = Constants.Variables.MESSAGE_TIMER;
                }
            }
        }
    } 
}
