using System;
using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    internal sealed class QuestObjectView : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private Track _trackDefault;
        [SerializeField] private Track _trackActive;

        private SpriteAnimator _spriteAnimator;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider2D;

        public int Id => _id;
        public Action<LevelObjectView> OnObjectContact;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Collider2D Collider2D => _collider2D;

        private void OnValidate()
        {
            _spriteRenderer = _spriteRenderer != null ? _spriteRenderer : GetComponent<SpriteRenderer>();
            _collider2D = _collider2D != null ? _collider2D : GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.TryGetComponent(out LevelObjectView levelObject);
            OnObjectContact?.Invoke(levelObject);
        }

        public void ProcessComplete()
        {
            _spriteAnimator.StartAnimation(SpriteRenderer, _trackActive, false, Constants.Variables.ANIMATIONS_SPEED);
        }

        public void ProcessActivate(SpriteAnimator spriteAnimator)
        {
            _spriteAnimator = spriteAnimator;
            _spriteAnimator.StartAnimation(SpriteRenderer, _trackDefault, true, Constants.Variables.ANIMATIONS_SPEED);
        }
    } 
}
