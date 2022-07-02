using System;

namespace GB_Platformer
{
    public interface IQuestStory : IDisposable
    {
        bool IsDone { get; }
    }
}