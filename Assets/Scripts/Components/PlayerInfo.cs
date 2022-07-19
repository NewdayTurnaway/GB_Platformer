using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class PlayerInfo
    {
        [SerializeField] private PlayerView _playerView;

        [Header("Settings")]
        [SerializeField] private float _runSpeed = 150;
        [SerializeField] private float _animationsSpeed = 10;
        [SerializeField] private float _jumpForce = 300;
        [SerializeField] private float _movingThreshold = 0.1f;
        [SerializeField] private float _flyThreshold = 0.1f;
        [SerializeField] private float _groundLevel = 0.1f;
        [SerializeField] private float _acceleration = -10f;
        [SerializeField] private float _damageAttack1 = 30f;
        [SerializeField] private float _damageAttack2 = 50f;
        [SerializeField] private Abilities _abilities;

        private bool _doSomething;
        private bool _isJump;

        public PlayerView PlayerView => _playerView;
        public SpriteRenderer PlayerSpriteRenderer => PlayerView.SpriteRenderer;
        public Rigidbody2D PlayerRigidbody2D => PlayerView.Rigidbody2D;
        public Collider2D PlayerCollider2D => PlayerView.Collider2D;

        public float RunSpeed => _runSpeed;
        public float AnimationsSpeed => _animationsSpeed;
        public float JumpForce => _jumpForce;
        public float MovingThreshold => _movingThreshold;
        public float FlyThreshold => _flyThreshold;
        public float GroundLevel => _groundLevel;
        public float Acceleration => _acceleration;
        public float DamageAttack1 => _damageAttack1;
        public float DamageAttack2 => _damageAttack2;
        
        public Abilities Abilities { get => _abilities; set => _abilities = value; }
        public bool DoSomething { get => _doSomething; set => _doSomething = value; }
        public bool InAir { get => _isJump; set => _isJump = value; }
    }
}