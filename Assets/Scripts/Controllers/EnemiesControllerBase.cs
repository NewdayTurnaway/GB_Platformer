using System.Collections.Generic;

namespace GB_Platformer
{
    internal abstract class EnemiesControllerBase: IInitialization, IFixedExecute, IDeinitialization
    {
        private protected readonly EnemiesInfo _enemiesInfo;
        private protected readonly SpriteAnimator _spriteAnimator;
        private protected readonly List<EnemyView> _enemyViews = new();

        protected EnemiesControllerBase(EnemiesInfo enemiesInfo, SpriteAnimator spriteAnimator)
        {
            _enemiesInfo = enemiesInfo;
            _spriteAnimator = spriteAnimator;
        }

        public abstract void Initialization();
        public virtual void Deinitialization()
        {
            _enemyViews.Clear();
        }
        public abstract void FixedExecute();

        private protected Track CheckEnemyTrackIdle(EnemyType enemyType)
        {
            if (enemyType == EnemyType.Patrol)
            {
                return Track.Skeleton_Idle;
            }
            return Track.FlyingEye_Flight;
        }

        private protected Track CheckEnemyTrackWalk(EnemyType enemyType)
        {
            if (enemyType == EnemyType.Patrol)
            {
                return Track.Skeleton_Walk;
            }
            return Track.FlyingEye_Flight;
        }
    }
}