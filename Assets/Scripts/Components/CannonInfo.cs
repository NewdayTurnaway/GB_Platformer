using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class CannonInfo
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _barrelTransform;
        [SerializeField] private Transform _emitterTransform;
        [SerializeField] private BulletInfo _bulletInfo;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Transform BarrelTransform => _barrelTransform;
        public Transform EmitterTransform => _emitterTransform;
        public BulletInfo BulletInfo => _bulletInfo;

    }
}