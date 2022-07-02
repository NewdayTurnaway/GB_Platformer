using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ActivateQuestLogic : IQuestLogic
    {
        public bool TryComplete(GameObject activator)
        {
            return activator.TryGetComponent<LevelObjectView>(out _);
        }
    }
}
