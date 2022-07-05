using System;
using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    internal sealed class QuestObjectView : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private int _id;
        [SerializeField] private Track _trackDefault;
        [SerializeField] private Track _trackActive;

        private SpriteAnimator _spriteAnimator;

        public int Id => _id;
        public Action<LevelObjectView> OnPlayerContact;
        public Action<int> QuestItem;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Collider2D Collider2D => _collider2D;
        public Transform Transform => _transform;

        private void OnValidate()
        {
            _transform = _transform != null ? _transform : GetComponent<Transform>();
            _spriteRenderer = _spriteRenderer != null ? _spriteRenderer : GetComponent<SpriteRenderer>();
            _collider2D = _collider2D != null ? _collider2D : GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out LevelObjectView playerView))
            {
                OnPlayerContact?.Invoke(playerView); 
            }
        }

        public void ProcessComplete()
        {
            _spriteAnimator.StartAnimation(SpriteRenderer, _trackActive, false, Constants.Variables.ANIMATIONS_SPEED);
            QuestItem?.Invoke(Id);
        }

        public void ProcessActivate(SpriteAnimator spriteAnimator)
        {
            _spriteAnimator = spriteAnimator;
            _spriteAnimator.StartAnimation(SpriteRenderer, _trackDefault, true, Constants.Variables.ANIMATIONS_SPEED);
        }
    } 
}
