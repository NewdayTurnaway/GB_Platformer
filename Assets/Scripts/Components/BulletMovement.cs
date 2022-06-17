using UnityEngine;

namespace GB_Platformer
{
    internal sealed class BulletMovement
    {
        private readonly Transform _targetTransform;

        public BulletMovement(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }

        public void MovementExecute(ProjectileView bullet)
        {
            if (!OnGround(bullet))
            {
                Vector3 direction = _targetTransform.position - bullet.transform.position;
                SetVelocity(bullet, bullet.Velocity + bullet.Acceleration * Time.deltaTime * -direction);
                bullet.transform.position += bullet.Velocity * Time.deltaTime;
                return;
            }

            SetVelocity(bullet, bullet.Velocity.Change(y: -bullet.Velocity.y));
            bullet.transform.position = bullet.transform.position.Change(y: bullet.GroundLevel + bullet.Radius);
        }

        public void Throw(ProjectileView bullet, Vector3 position, Vector3 velocity)
        {
            bullet.transform.position = position;
            SetVelocity(bullet, velocity);
        }

        private void SetVelocity(ProjectileView bullet, Vector3 velocity)
        {
            bullet.Velocity = velocity;

            float angle = Vector3.Angle(Vector3.left, bullet.Velocity);
            Vector3 axis = Vector3.Cross(Vector3.left, bullet.Velocity);
            bullet.transform.rotation = Quaternion.AngleAxis(angle, axis);
        }

        private bool OnGround(ProjectileView bullet)
        {
            return bullet.transform.position.y <= bullet.GroundLevel + bullet.Radius && bullet.Velocity.y <= 0;
        }
    }
}