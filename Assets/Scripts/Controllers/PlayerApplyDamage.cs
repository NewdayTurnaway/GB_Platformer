using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PlayerApplyDamage : IExecute
    {
        private readonly SpriteAnimator _spriteAnimator;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly float _animationsSpeed;
        private readonly Health _health;
        
        private float _savedHealth;
        private bool _death;

        public bool Death { get => _death; private set => _death = value; }

        public PlayerApplyDamage(PlayerInfo characterInfo, SpriteAnimator spriteAnimator)
        {
            _spriteAnimator = spriteAnimator;
            _spriteRenderer = characterInfo.PlayerView.SpriteRenderer;
            _animationsSpeed = characterInfo.AnimationsSpeed;
            _health = characterInfo.PlayerView.Health;
            _savedHealth = _health.CurrentHealth;
        }

        public void Execute()
        {
            bool changeHealth = _savedHealth != _health.CurrentHealth;
            _savedHealth = changeHealth ? _health.CurrentHealth : _savedHealth;

            if (Mathf.Approximately(_savedHealth, 0f))
            {
                Death = true;
                _spriteAnimator.StartAnimation(_spriteRenderer, Track.Death, false, _animationsSpeed);
                _spriteRenderer.gameObject.layer = LayerMask.NameToLayer(Constants.Layer.IGNORED);
                return;
            }

            if (changeHealth)
            {
                _spriteAnimator.StartAnimation(_spriteRenderer, Track.TakeHit, false, _animationsSpeed);
            }
        }
    }
}