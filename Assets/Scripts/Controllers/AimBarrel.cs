using UnityEngine;

namespace GB_Platformer
{
    internal sealed class AimBarrel : IExecute
    {
        private readonly CannonInfo _cannonInfo;
        private readonly Transform _barrelTransform;
        private readonly Transform _aimTransform;

        public AimBarrel(CannonInfo cannonInfo, Transform targetTransform)
        {
            _cannonInfo = cannonInfo;
            _barrelTransform = cannonInfo.BarrelTransform;
            _aimTransform = targetTransform;
        }

        public void Execute()
        {
            Vector3 direction = _aimTransform.position - _barrelTransform.position;
            float angle = Vector3.Angle(-Vector3.left, direction);
            Vector3 axis = Vector3.Cross(-Vector3.left, direction);

            _barrelTransform.rotation = Quaternion.AngleAxis(angle, axis);
        }
    }
}