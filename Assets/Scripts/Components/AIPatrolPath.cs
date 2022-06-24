using Pathfinding;
using System;

namespace GB_Platformer
{
    internal class AIPatrolPath : AIPath
    {
        public new Action TargetReached;

        public override void OnTargetReached()
        {
            base.OnTargetReached();
            DispatchTargetReached();
        }

        protected virtual void DispatchTargetReached()
        {
            TargetReached?.Invoke();
        }
    }
}