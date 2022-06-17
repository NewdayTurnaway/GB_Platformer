using UnityEngine;

namespace GB_Platformer
{
    internal sealed class BulletPhysicsMovement
    {
        private readonly Transform _targetTransform;

        public BulletPhysicsMovement(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }

        public void Throw(ProjectileView bullet, Vector3 position, Vector3 velocity)
        {
            Vector3 direction = _targetTransform.position - bullet.transform.position;
            bullet.transform.position = position;
            bullet.Rigidbody2D.velocity = Vector2.zero;
            bullet.Rigidbody2D.angularVelocity = 0;
            bullet.Rigidbody2D.AddForce(velocity + direction, ForceMode2D.Impulse);
        }
    }
}