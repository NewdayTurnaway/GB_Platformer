using Pathfinding;
using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    internal sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _targetWaypoint;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
        [SerializeField] private AIPatrolPath _protectorAIPatrolPath;

        public Health Health => _health;
        public Transform Transform => _transform;
        public Transform TargetWaypoint => _targetWaypoint;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public Collider2D Collider2D => _collider2D;
        
        public AIDestinationSetter ProtectorAIDestinationSetter => _protectorAIDestinationSetter;
        public AIPatrolPath ProtectorAIPatrolPath => _protectorAIPatrolPath;


        private void OnValidate()
        {
            _health = new(_health.MaxHealth);
            _transform = _transform != null ? _transform : GetComponent<Transform>();
            _spriteRenderer = _spriteRenderer != null ? _spriteRenderer : GetComponent<SpriteRenderer>();
            _rigidbody2D = _rigidbody2D != null ? _rigidbody2D : GetComponent<Rigidbody2D>();
            _collider2D = _collider2D != null ? _collider2D : GetComponent<Collider2D>();
            _protectorAIDestinationSetter = _protectorAIDestinationSetter != null ? _protectorAIDestinationSetter : TryGetComponent(out AIDestinationSetter q) ? q : null;
            _protectorAIPatrolPath = _protectorAIPatrolPath != null ? _protectorAIPatrolPath : TryGetComponent(out AIPatrolPath qq) ? qq : null;

            if (_transform.childCount >= 1)
            {
                _targetWaypoint = _targetWaypoint != null ? _targetWaypoint : _transform.GetChild(0).transform;
            }
        }

        public void TakeDamage(float damage)
        {
            Health.CurrentHealth -= damage;
        }
    }
}