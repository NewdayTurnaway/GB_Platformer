using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class RespawnController : IInitialization, IDeinitialization, IExecute
    {
        private readonly List<QuestObjectView> _resettableQuests = new();
        private readonly QuestStoryData _resettableQuestStoryData;
        private readonly PlayerView _playerView;
        private readonly EnemiesControllerBase _enemiesController;

        private Vector3 _currentRespawnPosition;
        private int _currentIndex;

        public RespawnController(QuestObjectView[] _questObjects, QuestStoryData resettableQuestStoryData, PlayerView playerView, EnemiesControllerBase enemiesController)
        {
            _playerView = playerView;
            _enemiesController = enemiesController;
            _resettableQuestStoryData = resettableQuestStoryData;

            foreach (QuestObjectView questObjectView in _questObjects)
            {
                if(questObjectView.Id >= 10)
                {
                    _resettableQuests.Add(questObjectView);
                }
            }

            _currentRespawnPosition = _playerView.Transform.position;
        }

        public void Initialization()
        {
            foreach (QuestObjectView questObjectView in _resettableQuests)
            {
                questObjectView.QuestItem += ChangeRespawnPoint;
            }
        }

        public void Deinitialization()
        {
            foreach (QuestObjectView questObjectView in _resettableQuests)
            {
                questObjectView.QuestItem -= ChangeRespawnPoint;
            }
        }

        private void ChangeRespawnPoint(int id)
        {
            if (_resettableQuestStoryData.Quests[_currentIndex].Id == id)
            {
                for (int i = 0; i < _resettableQuests.Count; i++)
                {
                    if (_resettableQuests[i].Id != id)
                    {
                        continue;
                    }
                    _currentRespawnPosition = _resettableQuests[i].Transform.position;
                    _currentIndex++;
                    return;
                }
            }
            _currentIndex = 0;
        }

        public void Execute()
        {
            if (_playerView.Death)
            {
                if (Input.GetKeyUp(KeyCode.R))
                {
                    _playerView.Death = false;
                    _playerView.ResetHeath();
                    _playerView.Transform.position = _currentRespawnPosition;
                    _playerView.gameObject.layer = LayerMask.NameToLayer(Constants.Layer.PLAYER);
                    _enemiesController.Reset();
                }
            }
        }
    }
}
