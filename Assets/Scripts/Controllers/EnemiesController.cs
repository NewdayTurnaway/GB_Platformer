using System.Collections.Generic;

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
                if (!_levelObjectTriggers.Contains(enemyInfo.ProtectedZoneTrigger))
                {
                    _levelObjectTriggers.Add(enemyInfo.ProtectedZoneTrigger);
                }
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
                float health = _enemiesHealth[i];
                ApplyDamage(ref health, _enemyViews[i], _enemiesInfo.EnemyInfos[i], out bool death);
                _enemiesHealth[i] = health;

                if (Death(death, i))
                {
                    continue;
                }

                if (_spriteAnimator.IsNotLooped(_enemyViews[i].SpriteRenderer))
                {
                    continue;
                }

                _spriteAnimator.StartAnimation(_enemyViews[i].SpriteRenderer, CheckEnemyTrack(_enemiesInfo.EnemyInfos[i].EnemyType, false), true, Constants.Variables.ANIMATIONS_SPEED);
                
                bool facingRight = _facingRightList[i];
                FlipHorizontally(ref facingRight, _enemyViews[i].ProtectorAIPatrolPath.velocity.x, _enemyViews[i].Transform);
                _facingRightList[i] = facingRight;
            }
        }

        private protected override bool Death(bool death, int index)
        {
            base.Death(death, index);
            if (death)
            {
                _enemiesInfo.EnemyInfos[index].EnemyView.ProtectorAIDestinationSetter.enabled = false;
                _enemiesInfo.EnemyInfos[index].EnemyView.ProtectorAIPatrolPath.enabled = false;
            }
            return death;
        }

        public override void Deinitialization()
        {
            base.Deinitialization();
            _enemiesHealth.Clear();
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