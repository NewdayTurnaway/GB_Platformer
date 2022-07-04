using System;
using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    internal class LevelObjectView : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Transform _transform;
        [SerializeField] protected Collider2D _collider2D;
        [SerializeField] protected Rigidbody2D _rigidbody2D;

        public Action<LevelObjectView> OnLevelObjectContact { get; set; }
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Transform Transform => _transform;
        public Collider2D Collider2D => _collider2D;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;

        protected virtual void OnValidate()
        {
            _spriteRenderer = _spriteRenderer != null ? _spriteRenderer : GetComponent<SpriteRenderer>();
            _transform = _transform != null ? _transform : GetComponent<Transform>();
            _collider2D = _collider2D != null ? _collider2D : GetComponent<Collider2D>();
            _rigidbody2D = _rigidbody2D != null ? _rigidbody2D : GetComponent<Rigidbody2D>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            collider.gameObject.TryGetComponent(out LevelObjectView levelObject);
            OnLevelObjectContact?.Invoke(levelObject);
            
            if(collider.gameObject.TryGetComponent(out PlayerView playerView))
            {
                playerView.AddCoin();
            }
        }
    } 
}
