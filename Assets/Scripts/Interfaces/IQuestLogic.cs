using UnityEngine;

namespace GB_Platformer
{
    public interface IQuestLogic
    {
        bool TryComplete(GameObject activator);
    }
}