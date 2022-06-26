using System;
using System.Collections.Generic;

namespace GB_Platformer
{
    internal sealed class EnemiesController : IInitialization, IFixedExecute, IDeinitialization
    {
        private readonly EnemiesInfo _enemiesInfo;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly List<EnemyView> _enemyViews = new();
        private readonly List<LevelObjectTrigger> _levelObjectTriggers = new();
        private readonly List<ProtectorAI> _protectorAIs = new();
        private readonly List<ProtectedZone> _protectedZones = new();

        public EnemiesController(EnemiesInfo enemiesInfo, LevelObjectView targetView, SpriteAnimator spriteAnimator)
        {
            _enemiesInfo = enemiesInfo;
            _spriteAnimator = spriteAnimator;
            foreach (EnemyInfo enemyInfo in _enemiesInfo.EnemyInfos)
            {
                _enemyViews.Add(enemyInfo.EnemyView);
                if (!_levelObjectTriggers.Contains(enemyInfo.ProtectedZoneTrigger))
                {
                    _levelObjectTriggers.Add(enemyInfo.ProtectedZoneTrigger);
                }
                _spriteAnimator.StartAnimation(enemyInfo.EnemyView.SpriteRenderer, CheckEnemyTrackIdle(enemyInfo.EnemyType), true, Constants.Variables.ANIMATIONS_SPEED);
                _protectorAIs.Add(new ProtectorAI(targetView, new PatrolAIModel(enemyInfo.Waypoints),enemyInfo.EnemyView.ProtectorAIDestinationSetter, enemyInfo.EnemyView.ProtectorAIPatrolPath));
            }
            foreach (LevelObjectTrigger levelObjectTrigger in _levelObjectTriggers)
            {
                List<IProtector> protectors = new();
                int count = 0;
                foreach (EnemyInfo enemyInfo in _enemiesInfo.EnemyInfos)
                {
                    if (ReferenceEquals(levelObjectTrigger, enemyInfo.ProtectedZoneTrigger))
                    {
                        protectors.Add(_protectorAIs[count]);
                    }
                    count++;
                }
                _protectedZones.Add(new ProtectedZone(levelObjectTrigger, protectors));
            }
        }

        public void Initialization()
        {
            foreach (ProtectorAI protectorAI in _protectorAIs)
            {
                protectorAI.Initialization();
            }
            foreach (ProtectedZone protectedZone in _protectedZones)
            {
                protectedZone.Initialization();
            }
        }

        public void FixedExecute()
        {
            for (int i = 0; i < _enemyViews.Count; i++)
            {
                _spriteAnimator.StartAnimation(_enemyViews[i].SpriteRenderer, CheckEnemyTrackWalk(_enemiesInfo.EnemyInfos[i].EnemyType), true, Constants.Variables.ANIMATIONS_SPEED);

                if(_enemyViews[i].ProtectorAIPatrolPath.velocity.x < 0)
                {
                    _enemyViews[i].SpriteRenderer.flipX = true;
                }
                else if (_enemyViews[i].ProtectorAIPatrolPath.velocity.x > 0)
                {
                    _enemyViews[i].SpriteRenderer.flipX = false;
                }
            }
        }

        public void Deinitialization()
        {
            foreach (ProtectorAI protectorAI in _protectorAIs)
            {
                protectorAI.Deinitialization();
            }
            foreach (ProtectedZone protectedZone in _protectedZones)
            {
                protectedZone.Deinitialization();
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