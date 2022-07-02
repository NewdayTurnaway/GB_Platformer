using System.Collections.Generic;
using System.Linq;

namespace GB_Platformer
{
    internal abstract class QuestStoryBase : IQuestStory
    {
        protected readonly List<IQuest> _questsCollection;
        public bool IsDone => _questsCollection.All(value => value.IsCompleted);

        protected QuestStoryBase(List<IQuest> questCollections)
        {
            _questsCollection = questCollections;
            Subscribe();
            ResetMethod();
        }

        protected virtual void ResetMethod(int index = 0) { }
        protected virtual void OnQuestCompleted(IQuest quest) { }

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

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}
