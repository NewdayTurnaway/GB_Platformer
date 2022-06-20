using UnityEngine;

namespace GB_Platformer
{
    internal sealed class AimBarrel : IExecute
    {
        private readonly Transform _barrelTransform;
        private readonly Transform _aimTransform;
        private readonly bool _isLowAngle;
        private readonly float _force;
        private readonly float _acceleration;

        public bool TargetInRange { get; private set; }

        public AimBarrel(CannonInfo cannonInfo, Transform targetTransform)
        {
            _barrelTransform = cannonInfo.BarrelTransform;
            _isLowAngle = cannonInfo.IsLowAngle;
            _force = cannonInfo.BulletInfo.Force;
            _acceleration = cannonInfo.BulletInfo.Acceleration;
            _aimTransform = targetTransform;
        }

        public void Execute()
        {
            Vector3 direction = _aimTransform.position - _barrelTransform.position;

            TargetInRange = LaunchAngle(_force, direction.magnitude, _acceleration, direction.y, out float highAngle, out float lowAngle);

            float currentAngle = _isLowAngle ? lowAngle : highAngle;

            Vector3 axis = Vector3.Cross(-Vector3.left, direction);
            _barrelTransform.rotation = Quaternion.AngleAxis(-Mathf.Sign(direction.x) * currentAngle * Mathf.Rad2Deg, axis);
        }

        public static bool LaunchAngle(float speed, float distance, float acceleration, float targetHeight, out float highAngle, out float lowAngle)
        {
            highAngle = lowAngle = 0;

            float sqrSpeed = speed * speed;
            float argumentA = Mathf.Pow(speed, 4);
            float argumentB = acceleration * (acceleration * (distance * distance) + (2 * targetHeight * sqrSpeed));

            if (argumentB > argumentA)
            {
                return false;
            }

            float root = Mathf.Sqrt(argumentA - argumentB);

            highAngle = Mathf.Atan((sqrSpeed + root) / (acceleration * distance));
            lowAngle = Mathf.Atan((sqrSpeed - root) / (acceleration * distance));
            return true;
        }
    }
}