using System;

namespace GB_Platformer
{
    public interface IQuest : IDisposable
    {
        event Action<IQuest> Completed;
        bool IsCompleted { get; }
        void Reset();
    }
}