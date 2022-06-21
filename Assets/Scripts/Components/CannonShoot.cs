using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class CannonShoot
    {
        private readonly SpriteAnimator _spriteAnimator;
        private readonly Transform _emitterTransform;
        private readonly BulletInfo _bulletInfo;
        private readonly IProjectileService _projectileService;
        private readonly BulletPhysicsMovement _bulletPhysicsMovement;

        private readonly Stack<ProjectileView> _stack = new();
        private float _timer;

        public CannonShoot(Transform targetTransform, Transform emitterTransform, BulletInfo bulletInfo, 
            IProjectileService viewServices, SpriteAnimator spriteAnimator)
        {
            _spriteAnimator = spriteAnimator;
            _emitterTransform = emitterTransform;
            _bulletInfo = bulletInfo;
            _projectileService = viewServices;
            _bulletPhysicsMovement = new();
        }

        public void MovementUpdate()
        {
            foreach(ProjectileView item in _stack)
            {
                if (item.gameObject.activeInHierarchy)
                {
                    CheckDestroy(item);
                }
            }
        }

        public void Shoot()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _timer = _bulletInfo.Delay;
                ProjectileView bullet = _projectileService.Instantiate<ProjectileView>(_bulletInfo.Prefab);
                _spriteAnimator.StartAnimation(bullet.SpriteRenderer, Track.Projectile, true, Constants.Variables.ANIMATIONS_SPEED);
                bullet.SetAllFields(_bulletInfo);
                _bulletPhysicsMovement.Throw(bullet, _emitterTransform.position, _emitterTransform.up * _bulletInfo.Force);
                if (_stack.Contains(bullet))
                {
                    return;
                }
                bullet.DeathTimer = bullet.TimeDestroy;
                _stack.Push(bullet);
            }
        }

        public void CheckDestroy(ProjectileView tempBullet)
        {
            if (tempBullet.DeathTimer > 0)
            {
                tempBullet.DeathTimer -= Time.deltaTime;
            }
            else
            {
                tempBullet.DeathTimer = _bulletInfo.TimeDestroy;

                tempBullet.transform.position = _emitterTransform.transform.position;
                _spriteAnimator.StopAnimation(tempBullet.SpriteRenderer);
                _projectileService.Destroy(tempBullet.gameObject);
            }
        }
    }
}