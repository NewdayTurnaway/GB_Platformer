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
                _enemyViews.Add(enemyInfo.EnemyView);
                _spriteAnimator.StartAnimation(enemyInfo.EnemyView.SpriteRenderer, CheckEnemyTrackIdle(enemyInfo.EnemyType), true, Constants.Variables.ANIMATIONS_SPEED);
                _simplePatrolAIs.Add(new SimplePatrolAI(enemyInfo.EnemyView, new SimplePatrolAIModel(enemyInfo)));
            }
        }

        public override void Initialization() { }

        public override void FixedExecute()
        {
            for (int i = 0; i < _enemyViews.Count; i++)
            {
                _simplePatrolAIs[i].FixedExecute();
                _spriteAnimator.StartAnimation(_enemyViews[i].SpriteRenderer, CheckEnemyTrackWalk(_enemiesInfo.EnemyInfos[i].EnemyType), true, Constants.Variables.ANIMATIONS_SPEED);
                _enemyViews[i].SpriteRenderer.flipX = _enemyViews[i].Rigidbody2D.velocity.x < 0;
            }
        }

        public override void Deinitialization()
        {
            base.Deinitialization();
            _simplePatrolAIs.Clear();
        }
    }
}