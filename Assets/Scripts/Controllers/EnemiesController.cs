using System.Collections.Generic;

namespace GB_Platformer
{
    internal sealed class EnemiesController : EnemiesControllerBase, IExecute
    {
        private readonly List<LevelObjectTrigger> _levelObjectTriggers = new();
        private readonly List<ProtectorAI> _protectorAIs = new();
        private readonly List<ProtectedZone> _protectedZones = new();
        private readonly List<EnemyAttack> _enemyAttacks = new();

        public EnemiesController(EnemiesInfo enemiesInfo, LevelObjectView targetView, SpriteAnimator spriteAnimator) : base(enemiesInfo, spriteAnimator)
        {
            for (int i = 0; i < _enemiesInfo.EnemyInfos.Count; i++)
            {
                EnemyInfo enemyInfo = _enemiesInfo.EnemyInfos[i];
                if (!_levelObjectTriggers.Contains(enemyInfo.ProtectedZoneTrigger))
                {
                    _levelObjectTriggers.Add(enemyInfo.ProtectedZoneTrigger);
                }
                _protectorAIs.Add(new ProtectorAI(targetView.Transform, enemyInfo.EnemyView, enemyInfo, enemyInfo.EnemyType, new PatrolAIModel(enemyInfo.Waypoints)));
                _enemyAttacks.Add(new EnemyAttack(enemyInfo, _spriteAnimator));
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
            base.Initialization();
            foreach (ProtectorAI protectorAI in _protectorAIs)
            {
                protectorAI.Initialization();
            }
            foreach (ProtectedZone protectedZone in _protectedZones)
            {
                protectedZone.Initialization();
            }
        }

        public void Execute()
        {
            foreach (EnemyAttack enemyAttack in _enemyAttacks)
            {
                enemyAttack.Execute();
            }
            for (int i = 0; i < _enemyViews.Count; i++)
            {
                if (_spriteAnimator.IsNotLooped(_enemyViews[i].SpriteRenderer))
                {
                    _enemyViews[i].ProtectorAIDestinationSetter.enabled = false;
                    _enemyViews[i].ProtectorAIPatrolPath.enabled = false;
                    continue;
                }
                if (UnityEngine.Mathf.Approximately(_enemiesHealth[i], 0f))
                {
                    continue;
                }
                _enemyViews[i].ProtectorAIDestinationSetter.enabled = true;
                _enemyViews[i].ProtectorAIPatrolPath.enabled = true;
            }
            
        }

        public override void FixedExecute()
        {
            foreach (ProtectorAI protectorAI in _protectorAIs)
            {
                protectorAI.FixedExecute();
            }
            for (int i = 0; i < _enemyViews.Count; i++)
            {
                float health = _enemiesHealth[i];
                ApplyDamage(ref health, _enemyViews[i], _enemiesInfo.EnemyInfos[i], out bool death);
                _enemiesHealth[i] = health;

                if (Death(death, i))
                {
                    _enemiesInfo.EnemyInfos[i].IsDeath = true;
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

        public override void Reset()
        {
            base.Reset();
            foreach (ProtectorAI protectorAI in _protectorAIs)
            {
                protectorAI.ResetPosition();
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