using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class BulletInfo
    {
        [SerializeField] private Sprite _sprite;

        [Header("Settings")]
        [SerializeField] private float _spriteSize = 3f;
        [SerializeField] private float _delay = 1;
        [SerializeField] private float _timeDestroy = 3;
        [SerializeField] private float _startSpeed = 5;
        [SerializeField] private float _radius = 0.3f;
        [SerializeField] private float _groundLevel = 0;
        [SerializeField] private float _acceleration = -10;

        public Sprite Sprite => _sprite;
        public float SpriteSize => _spriteSize;
        public float Radius => _radius;
        public float Delay => _delay;
        public float TimeDestroy => _timeDestroy;
        public float StartSpeed => _startSpeed;
        public float GroundLevel => _groundLevel;
        public float Acceleration => _acceleration;
    }
}