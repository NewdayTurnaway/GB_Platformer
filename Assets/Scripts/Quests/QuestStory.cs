using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class QuestStory : IQuestStory
    {
        private readonly List<IQuest> _questsCollection;
        public bool IsDone => _questsCollection.All(value => value.IsCompleted);

        public QuestStory(List<IQuest> questCollections)
        {
            _questsCollection = questCollections;
            Subscribe();
            ResetQuest(0);
        }

        private void Subscribe()
        {
            foreach (IQuest quest in _questsCollection)
            {
                quest.Completed += OnQuestCompleted;
            }
        }

        private void Unsubscribe()
        {
            foreach (IQuest quest in _questsCollection)
            {
                quest.Completed -= OnQuestCompleted;
            }
        }

        private void OnQuestCompleted(IQuest quest)
        {
            int index = _questsCollection.IndexOf(quest);
            if (!IsDone)
            {
                ResetQuest(++index);
                return;
            }
            Debug.Log("Complete");
        }

        private void ResetQuest(int index)
        {
            if (index < 0 || index >= _questsCollection.Count)
            {
                return;
            }

            IQuest nextQuest = _questsCollection[index];
            if (nextQuest.IsCompleted)
            {
                OnQuestCompleted(nextQuest);
            }
            else
            {
                _questsCollection[index].Reset();
            }
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}
