using System;
using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    internal sealed class QuestObjectView : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private Track _track;
        [SerializeField] private Sprite _sprite;
        private SpriteAnimator _spriteAnimator;

        public int Id => _id;
        public Action<LevelObjectView> OnObjectContact;

        public SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.TryGetComponent(out LevelObjectView levelObject);
            OnObjectContact?.Invoke(levelObject);
        }

        public void ProcessComplete()
        {
            _spriteAnimator.StartAnimation(SpriteRenderer, _track, false, Constants.Variables.ANIMATIONS_SPEED);
        }

        public void ProcessActivate(SpriteAnimator spriteAnimator)
        {
            SpriteRenderer.sprite = _sprite;
            _spriteAnimator = spriteAnimator;
        }
    } 
}
