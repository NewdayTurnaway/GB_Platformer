using System;
using System.Collections.Generic;

namespace GB_Platformer
{
    internal sealed class EnemiesSimpleController : IFixedExecute
    {
        private readonly EnemiesInfo _enemiesInfo;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly List<EnemyView> _enemyViews = new();
        private readonly List<SimplePatrolAI> _simplePatrolAIs = new();

        public EnemiesSimpleController(EnemiesInfo enemiesInfo, SpriteAnimator spriteAnimator)
        {
            _enemiesInfo = enemiesInfo;
            _spriteAnimator = spriteAnimator;
            foreach (EnemyInfo enemyInfo in _enemiesInfo.EnemyInfos)
            {
                _enemyViews.Add(enemyInfo.EnemyView);
                _spriteAnimator.StartAnimation(enemyInfo.EnemyView.SpriteRenderer, CheckEnemyTrackIdle(enemyInfo.EnemyType), true, Constants.Variables.ANIMATIONS_SPEED);
                _simplePatrolAIs.Add(new SimplePatrolAI(enemyInfo.EnemyView, new SimplePatrolAIModel(enemyInfo)));
            }
        }

        public void FixedExecute()
        {
            for (int i = 0; i < _enemyViews.Count; i++)
            {
                _simplePatrolAIs[i].FixedExecute();
                _spriteAnimator.StartAnimation(_enemyViews[i].SpriteRenderer, CheckEnemyTrackWalk(_enemiesInfo.EnemyInfos[i].EnemyType), true, Constants.Variables.ANIMATIONS_SPEED);
                _enemyViews[i].SpriteRenderer.flipX = _enemyViews[i].Rigidbody2D.velocity.x < 0;
            }
        }

        private Track CheckEnemyTrackIdle(EnemyType enemyType)
        {
            if (enemyType == EnemyType.Patrol)
            {
                return Track.Skeleton_Idle;
            }
            return Track.FlyingEye_Flight;
        }

        private Track CheckEnemyTrackWalk(EnemyType enemyType)
        {
            if (enemyType == EnemyType.Patrol)
            {
                return Track.Skeleton_Walk;
            }
            return Track.FlyingEye_Flight;
        }
    }
}