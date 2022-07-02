using System.Collections.Generic;

namespace GB_Platformer
{
    internal sealed class EnemiesSimpleController : EnemiesControllerBase
    {
        private readonly List<SimplePatrolAI> _simplePatrolAIs = new();

        public EnemiesSimpleController(EnemiesInfo enemiesInfo, SpriteAnimator spriteAnimator) : base(enemiesInfo, spriteAnimator)
        {
            foreach (EnemyInfo enemyInfo in _enemiesInfo.EnemyInfos)
            {
                _simplePatrolAIs.Add(new SimplePatrolAI(enemyInfo.EnemyView, new SimplePatrolAIModel(enemyInfo)));
            }
        }

        public override void Initialization() { }

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

                _simplePatrolAIs[i].FixedExecute();
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
                _simplePatrolAIs.Remove(_simplePatrolAIs[index]);
            }
            return death;
        }

        public override void Deinitialization()
        {
            base.Deinitialization();
            _simplePatrolAIs.Clear();
        }
    }
}