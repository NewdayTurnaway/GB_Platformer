using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PlayerAttack : IExecute
    {
        private readonly PlayerInfo _characterInfo;
        private readonly SpriteAnimator _spriteAnimator;

        public PlayerAttack(PlayerInfo characterInfo, SpriteAnimator spriteAnimator)
        {
            _characterInfo = characterInfo;
            _spriteAnimator = spriteAnimator;
        }

        public void Execute()
        {
            if(_characterInfo.InAir)
            {
                return;
            }
            Attack(Constants.Input.ATTACK1, Track.Attack1, _characterInfo.DamageAttack1);
            Attack(Constants.Input.ATTACK2, Track.Attack2, _characterInfo.DamageAttack2);
        }

        private void Attack(string inputButton, Track track, float damage)
        {
            if (Input.GetButtonDown(inputButton))
            {
                _spriteAnimator.StartAnimation(_characterInfo.PlayerSpriteRenderer, track, false, _characterInfo.AnimationsSpeed);

                Collider2D[] hitEnemies = new Collider2D[3];
                Physics2D.OverlapCircleNonAlloc(_characterInfo.PlayerView.AttackPointTransform.position, 
                    _characterInfo.PlayerView.AttackRange, hitEnemies, LayerMask.GetMask(Constants.Layer.ENEMY));

                foreach (Collider2D collider2D in hitEnemies)
                {
                    if (collider2D != null && collider2D.TryGetComponent(out EnemyView enemy))
                    {
                        enemy.TakeDamage(damage);
                    }
                }
            }
        }
    }
}