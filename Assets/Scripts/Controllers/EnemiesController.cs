using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class EnemiesController : EnemiesControllerBase
    {
        private readonly List<LevelObjectTrigger> _levelObjectTriggers = new();
        private readonly List<ProtectorAI> _protectorAIs = new();
        private readonly List<ProtectedZone> _protectedZones = new();

        public EnemiesController(EnemiesInfo enemiesInfo, LevelObjectView targetView, SpriteAnimator spriteAnimator) : base(enemiesInfo, spriteAnimator)
        {
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
                for (int i = 0; i < _enemiesInfo.EnemyInfos.Count; i++)
                {
                    if (ReferenceEquals(levelObjectTrigger, _enemiesInfo.EnemyInfos[i].ProtectedZoneTrigger))
                    {
                        protectors.Add(_protectorAIs[i]);
                    }
                }
                _protectedZones.Add(new ProtectedZone(levelObjectTrigger, protectors));
            }
        }

        public override void Initialization()
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

        public override void FixedExecute()
        {
            for (int i = 0; i < _enemyViews.Count; i++)
            {
                _spriteAnimator.StartAnimation(_enemyViews[i].SpriteRenderer, CheckEnemyTrackWalk(_enemiesInfo.EnemyInfos[i].EnemyType), true, Constants.Variables.ANIMATIONS_SPEED);

                if (Mathf.Approximately(_enemyViews[i].ProtectorAIPatrolPath.velocity.x, 0))
                {
                    return;
                }
                _enemyViews[i].SpriteRenderer.flipX = _enemyViews[i].ProtectorAIPatrolPath.velocity.x < 0;
            }
        }

        public override void Deinitialization()
        {
            base.Deinitialization();
            _levelObjectTriggers.Clear();
            foreach (ProtectorAI protectorAI in _protectorAIs)
            {
                protectorAI.Deinitialization();
            }
            _protectorAIs.Clear();
            foreach (ProtectedZone protectedZone in _protectedZones)
            {
                protectedZone.Deinitialization();
            }
            _protectedZones.Clear();
        }
    }
}