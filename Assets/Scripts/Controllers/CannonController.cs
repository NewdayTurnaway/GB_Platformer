using UnityEngine;

namespace GB_Platformer
{
    internal sealed class CannonController : IExecute
    {
        private readonly CannonInfo _cannonInfo;
        private readonly CannonShoot _cannonShoot;
        private readonly AimBarrel _aimBarrel;

        public CannonController(CannonInfo cannonInfo, Transform targetTransform, SpriteAnimator spriteAnimator)
        {
            _cannonInfo = cannonInfo;
            _aimBarrel = new(_cannonInfo, targetTransform);
            _cannonShoot = new(targetTransform, _cannonInfo.EmitterTransform, _cannonInfo.BulletInfo, new ProjectileService(), spriteAnimator);
        }

        public void Execute()
        {
            _aimBarrel.Execute();
            if(_aimBarrel.TargetInRange)
            {
                _cannonShoot.Shoot();
            }
            _cannonShoot.MovementUpdate();
        }
    }
}