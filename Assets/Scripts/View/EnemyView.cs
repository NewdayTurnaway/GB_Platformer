using Pathfinding;
using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    internal sealed class EnemyView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        private AIDestinationSetter _protectorAIDestinationSetter;
        private AIPatrolPath _protectorAIPatrolPath;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public Collider2D Collider2D => _collider2D;
        
        public AIDestinationSetter ProtectorAIDestinationSetter => _protectorAIDestinationSetter;

        public AIPatrolPath ProtectorAIPatrolPath => _protectorAIPatrolPath;

        private void OnValidate()
        {
            _spriteRenderer = _spriteRenderer != null ? _spriteRenderer : GetComponent<SpriteRenderer>();
            _rigidbody2D = _rigidbody2D != null ? _rigidbody2D : GetComponent<Rigidbody2D>();
            _collider2D = _collider2D != null ? _collider2D : GetComponent<Collider2D>();
            _protectorAIDestinationSetter = _protectorAIDestinationSetter != null ? _protectorAIDestinationSetter : TryGetComponent(out AIDestinationSetter q) ? q : null;
            _protectorAIPatrolPath = _protectorAIPatrolPath != null ? _protectorAIPatrolPath : TryGetComponent(out AIPatrolPath qq) ? qq : null;
        }
    }
}