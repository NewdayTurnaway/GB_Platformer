using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class PlayerInfo
    {
        [SerializeField] private LevelObjectView _playerLevelObjectView;
        [SerializeField] private Transform _headTransform;

        [Header("Settings")]
        [SerializeField] private float _runSpeed = 150;
        [SerializeField] private float _animationsSpeed = 10;
        [SerializeField] private float _jumpForce = 300;
        [SerializeField] private float _movingTresh = 0.1f;
        [SerializeField] private float _flyTresh = 0.1f;
        [SerializeField] private float _groundLevel = 0.1f;
        [SerializeField] private float _acceleration = -10f;

        private bool _doSomething;

        public LevelObjectView PlayerLevelObjectView => _playerLevelObjectView;
        public SpriteRenderer PlayerSpriteRenderer => PlayerLevelObjectView.SpriteRenderer;
        public Rigidbody2D PlayerRigidbody2D => PlayerLevelObjectView.Rigidbody2D;
        public Collider2D PlayerCollider2D => PlayerLevelObjectView.Collider2D;
        public Transform HeadTransform => _headTransform;

        public float RunSpeed => _runSpeed;
        public float AnimationsSpeed => _animationsSpeed;
        public float JumpForce => _jumpForce;
        public float MovingTresh => _movingTresh;
        public float FlyTresh => _flyTresh;
        public float GroundLevel => _groundLevel;
        public float Acceleration => _acceleration;

        public bool DoSomething { get => _doSomething; set => _doSomething = value; }
    }
}