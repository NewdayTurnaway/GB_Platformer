using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal abstract class EnemiesControllerBase: IInitialization, IFixedExecute, IDeinitialization
    {
        private protected readonly EnemiesInfo _enemiesInfo;
        private protected readonly SpriteAnimator _spriteAnimator;
        private protected readonly List<EnemyView> _enemyViews = new();
        private protected readonly List<float> _enemiesHealth = new();
        private protected List<bool> _facingRightList = new();

        protected EnemiesControllerBase(EnemiesInfo enemiesInfo, SpriteAnimator spriteAnimator)
        {
            _enemiesInfo = enemiesInfo;
            _spriteAnimator = spriteAnimator;
            foreach (EnemyInfo enemyInfo in _enemiesInfo.EnemyInfos)
            {
                _enemyViews.Add(enemyInfo.EnemyView);
                _enemiesHealth.Add(enemyInfo.EnemyView.Health.CurrentHealth);
                _facingRightList.Add(false);
                _spriteAnimator.StartAnimation(enemyInfo.EnemyView.SpriteRenderer, CheckEnemyTrack(enemyInfo.EnemyType), true, Constants.Variables.ANIMATIONS_SPEED);
            }
        }

        public virtual void Initialization()
        {
            foreach (EnemyView enemyView in _enemyViews)
            {
                enemyView.ResetHeath();
            }
        }
        public virtual void Deinitialization()
        {
            _enemyViews.Clear();
        }
        public abstract void FixedExecute();

        public virtual void Reset()
        {
            for (int i = 0; i < _enemiesHealth.Count; i++)
            {
                _enemyViews[i].Health.CurrentHealth = _enemiesInfo.EnemyInfos[i].EnemyView.Health.MaxHealth;
                _enemiesHealth[i] = _enemiesInfo.EnemyInfos[i].EnemyView.Health.MaxHealth;
                _enemyViews[i].gameObject.layer = LayerMask.NameToLayer(Constants.Layer.ENEMY);
            }
        }

        private protected void ApplyDamage(ref float savedHealth, EnemyView enemyView, EnemyInfo enemyInfo, out bool death)
        {
            death = false;
            bool changeHealth = savedHealth != enemyView.Health.CurrentHealth;
            savedHealth = changeHealth ? enemyView.Health.CurrentHealth : savedHealth;
            if (Mathf.Approximately(savedHealth, 0f))
            {
                death = true;
                _spriteAnimator.StartAnimation(enemyView.SpriteRenderer, CheckEnemyTrack(enemyInfo.EnemyType, false, false, true), false, Constants.Variables.ANIMATIONS_SPEED);
                return;
            }
            if (changeHealth)
            {
                _spriteAnimator.StartAnimation(enemyView.SpriteRenderer, CheckEnemyTrack(enemyInfo.EnemyType, false, true), false, Constants.Variables.ANIMATIONS_SPEED);
            }
        }

        private protected virtual bool Death(bool death, int index)
        {
            if (death)
            {
                _enemyViews[index].gameObject.layer = LayerMask.NameToLayer(Constants.Layer.IGNORED);
                _enemyViews[index].Rigidbody2D.velocity = Vector3.zero;
            }
            return death;
        }

        private protected void FlipHorizontally(ref bool facingRight, float velocityX, Transform viewTransform)
        {
            if ((velocityX < 0 && !facingRight) || (velocityX > 0 && facingRight))
            {
                Flip(ref facingRight, viewTransform);
            }
        }

        private void Flip(ref bool facingRight, Transform viewTransform)
        {
            Vector3 newVector = viewTransform.localScale;
            newVector.x *= -1;
            viewTransform.localScale = newVector;

            facingRight = !facingRight;
        }

        private protected Track CheckEnemyTrack(EnemyType enemyType, bool idle = true, bool hit = false, bool death = false)
        {
            if (!hit && !death)
            {
                return enemyType switch
                {
                    EnemyType.Patrol => idle ? Track.Skeleton_Idle: Track.Skeleton_Walk,
                    EnemyType.FlyPatrol => Track.FlyingEye_Flight,
                    _ => Track.None,
                };
            }
            if (hit)
            {
                return enemyType switch
                {
                    EnemyType.Patrol => Track.Skeleton_TakeHit,
                    EnemyType.FlyPatrol => Track.FlyingEye_TakeHit,
                    _ => Track.None,
                };
            }
            if (death)
            {
                return enemyType switch
                {
                    EnemyType.Patrol => Track.Skeleton_Death,
                    EnemyType.FlyPatrol => Track.FlyingEye_Death,
                    _ => Track.None,
                }; 
            }
            return Track.None;
        }
    }
}