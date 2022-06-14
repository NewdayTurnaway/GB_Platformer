using System.Collections;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class BulletMovement
    {
        private readonly BulletInfo _bulletInfo;
        private Vector3 _velocity;
        private GameObject _bullet;
        private readonly Transform _targetTransform;


        public BulletMovement(BulletInfo bulletInfo, Transform targetTransform)
        {
            _bulletInfo = bulletInfo;
            _targetTransform = targetTransform;
        }

        public void MovementExecute(GameObject bullet)
        {
            if (!OnGround(bullet))
            {
                Vector3 direction = _targetTransform.position - bullet.transform.position;
                SetVelocity(bullet, _velocity + -direction * _bulletInfo.Acceleration * Time.deltaTime);
                bullet.transform.position += _velocity * Time.deltaTime;
                return;
            }

            SetVelocity(bullet, _velocity.Change(y: -_velocity.y));
            bullet.transform.position = bullet.transform.position.Change(y: _bulletInfo.GroundLevel + _bulletInfo.Radius);
        }

        public void Throw(GameObject bullet, Vector3 position, Vector3 velocity)
        {
            bullet.transform.position = position;
            SetVelocity(bullet, velocity);
        }

        private void SetVelocity(GameObject bullet, Vector3 velocity)
        {
            _velocity = velocity;

            float angle = Vector3.Angle(Vector3.left, _velocity);
            Vector3 axis = Vector3.Cross(Vector3.left, _velocity);
            bullet.transform.rotation = Quaternion.AngleAxis(angle, axis);
        }

        private bool OnGround(GameObject bullet)
        {
            return bullet.transform.position.y <= _bulletInfo.GroundLevel + _bulletInfo.Radius && _velocity.y <= 0;
        }
    }
}