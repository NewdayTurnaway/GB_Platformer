using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ResettableQuestStory : QuestStoryBase
    {
        private int _currentIndex;

        public ResettableQuestStory(List<IQuest> questCollections) : base(questCollections)
        {
        }

        protected override void ResetMethod(int index = 0)
        {
            _currentIndex = 0;

            foreach (IQuest quest in _questsCollection)
            {
                quest.Reset();
            }
        }

        protected override void OnQuestCompleted(IQuest quest)
        {
            int index = _questsCollection.IndexOf(quest);

            if (_currentIndex != index)
            {
                ResetMethod();
                return;
            }
            _currentIndex++;

            if (!IsDone)
            {
                return;
            }

            Debug.Log("Complete");
        }
    }
}
