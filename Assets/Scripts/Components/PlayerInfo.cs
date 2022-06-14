using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class PlayerInfo
    {
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;
        [SerializeField] private SpriteAnimationsData _playerAnimationsData;

        [Header("Settings")]
        [SerializeField] private float _walkSpeed = 1;
        [SerializeField] private float _animationsSpeed = 3;
        [SerializeField] private float _jumpStartSpeed = 2;
        [SerializeField] private float _movingTresh = 0.1f;
        [SerializeField] private float _flyTresh = 0.3f;
        [SerializeField] private float _groundLevel = 0.1f;
        [SerializeField] private float _acceleration = -10f;

        private bool _doSomething;

        public SpriteRenderer PlayerSpriteRenderer => _playerSpriteRenderer;
        public SpriteAnimationsData PlayerAnimationsData => _playerAnimationsData;

        public float WalkSpeed => _walkSpeed;
        public float AnimationsSpeed => _animationsSpeed;
        public float JumpStartSpeed => _jumpStartSpeed;
        public float MovingTresh => _movingTresh;
        public float FlyTresh => _flyTresh;
        public float GroundLevel => _groundLevel;
        public float Acceleration => _acceleration;

        public bool DoSomething { get => _doSomething; set => _doSomething = value; }
    }
}