using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class CannonShoot
    {
        private readonly Transform _emitterTransform;
        private readonly BulletInfo _bulletInfo;
        private readonly IProjectileBulletService _projectileService;
        private readonly BulletMovement _bulletMovement;

        private readonly Stack<GameObject> _stack = new();
        private float _timer;
        private float _timerDestroy;

        public CannonShoot(Transform targetTransform, Transform emitterTransform, BulletInfo bulletInfo, IProjectileBulletService viewServices)
        {
            _emitterTransform = emitterTransform;
            _bulletInfo = bulletInfo;
            _projectileService = viewServices;
            _timerDestroy = _bulletInfo.TimeDestroy;
            _bulletMovement = new(_bulletInfo, targetTransform);
        }

        public void MovementUpdate()
        {
            CheckDestroy();
            if (_stack.TryPop(out GameObject tempBullet))
            {
                _bulletMovement.MovementExecute(tempBullet);
                _stack.Push(tempBullet);
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
                GameObject bullet = _projectileService.InstantiateBullet<SpriteRenderer>(_bulletInfo).gameObject;
                _bulletMovement.Throw(bullet, _emitterTransform.position, _emitterTransform.up * _bulletInfo.StartSpeed);
                _stack.Push(bullet);
            }
        }

        public void CheckDestroy()
        {
            if (_timerDestroy > 0)
            {
                _timerDestroy -= Time.deltaTime;
            }
            else
            {
                _timerDestroy = _bulletInfo.TimeDestroy;
                
                if(_stack.TryPop(out GameObject tempBullet))
                {
                    tempBullet.transform.position = _emitterTransform.transform.position;
                    _projectileService.Destroy(tempBullet);
                } 
            }
        }
    }
}