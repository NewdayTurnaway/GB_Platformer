using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    internal sealed class ProjectileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _delay = 1;
        [SerializeField] private float _timeDestroy = 3;
        [SerializeField] private float _force = 5;
        [SerializeField] private float _radius = 0.03f;
        [SerializeField] private float _groundLevel = 0;
        [SerializeField] private float _acceleration = -10;
        [SerializeField] private float _damage = 10;

        private float _deathTimer;
        private Vector3 _velocity;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public float DeathTimer { get => _deathTimer; set => _deathTimer = value; }
        public Vector3 Velocity { get => _velocity; set => _velocity = value; }
        public float Delay { get => _delay; set => _delay = value; }
        public float TimeDestroy { get => _timeDestroy; set => _timeDestroy = value; }
        public float Force { get => _force; set => _force = value; }
        public float Radius { get => _radius; set => _radius = value; }
        public float GroundLevel { get => _groundLevel; set => _groundLevel = value; }
        public float Acceleration { get => _acceleration; set => _acceleration = value; }
        public float Damage { get => _damage; set => _damage = value; }

        private void OnValidate()
        {
            _spriteRenderer = _spriteRenderer != null ? _spriteRenderer : GetComponent<SpriteRenderer>();
            _rigidbody2D = _rigidbody2D != null ? _rigidbody2D : GetComponent<Rigidbody2D>();
        }

        public void SetAllFields(BulletInfo bulletInfo)
        {
            Delay = bulletInfo.Delay;
            TimeDestroy = bulletInfo.TimeDestroy;
            Force = bulletInfo.Force;
            Radius = bulletInfo.Radius;
            GroundLevel = bulletInfo.GroundLevel;
            Acceleration = bulletInfo.Acceleration;
            Damage = bulletInfo.Damage;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision != null && collision.gameObject.TryGetComponent(out PlayerView player))
            {
                player.TakeDamage(Damage);
                DeathTimer = 0;
            }
        }
    } 
}
