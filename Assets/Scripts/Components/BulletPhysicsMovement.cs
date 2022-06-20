using UnityEngine;

namespace GB_Platformer
{
    internal sealed class BulletPhysicsMovement
    {
        public void Throw(ProjectileView bullet, Vector3 position, Vector3 velocity)
        {
            bullet.transform.position = position;
            bullet.Rigidbody2D.velocity = Vector2.zero;
            bullet.Rigidbody2D.angularVelocity = 0;
            bullet.Rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}