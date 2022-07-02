using System;
using System.Collections.Generic;
using System.Linq;

namespace GB_Platformer
{
    internal sealed class QuestsConfigurator : IDeinitialization
    {
        private readonly SpriteAnimator _spriteAnimator;

        private readonly QuestStoryData[] _questStoriesData;
        private readonly QuestObjectView[] _questObjects;

        private readonly Dictionary<QuestType, Func<IQuestLogic>> _questFactories = new()
        {
            { QuestType.Activate, () => new ActivateQuestLogic() },
        };

        private readonly Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories = new()
        {
            { QuestStoryType.Common, questCollection => new CommonQuestStory(questCollection) },
            { QuestStoryType.Resettable, questCollection => new ResettableQuestStory(questCollection) },
        };

        private readonly List<IQuestStory> _questStories = new();

        public QuestsConfigurator(QuestStoryData[] questStoriesData, QuestObjectView[] questObjects, SpriteAnimator spriteAnimator)
        {
            _spriteAnimator = spriteAnimator;
            _questStoriesData = questStoriesData;
            _questObjects = questObjects;

            foreach (QuestStoryData questStoryData in _questStoriesData)
            {
                _questStories.Add(CreateQuestStory(questStoryData));
            }
        }

        private IQuestStory CreateQuestStory(QuestStoryData questStoryData)
        {
            List<IQuest> quests = new();
            foreach (QuestData questData in questStoryData.Quests)
            {
                IQuest quest = CreateQuest(questData);
                if(quest == null)
                {
                    continue;
                }
                quests.Add(quest);
            }
            return _questStoryFactories[questStoryData.QuestStoryType].Invoke(quests);
        }

        private IQuest CreateQuest(QuestData questData)
        {
            int questId = questData.Id;
            QuestObjectView questView = _questObjects.FirstOrDefault(value => value.Id == questId);
            if (questView == null)
            { 
                return null;
            }
            if (_questFactories.TryGetValue(questData.QuestType, out Func<IQuestLogic> factory))
            {
                IQuestLogic questLogic = factory.Invoke();
                return new Quest(questView, questLogic, _spriteAnimator);
            }
            return null;
        }

        public void Deinitialization()
        {
            foreach (IQuestStory questStory in _questStories)
            {
                questStory.Dispose();
            }
            _questStories.Clear();
        }
    } 
}
