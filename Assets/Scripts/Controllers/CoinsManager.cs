using System;
using System.Collections.Generic;

namespace GB_Platformer
{
    internal sealed class CoinsManager : IDisposable
    {
        private readonly SpriteAnimator _spriteAnimator;

        private readonly LevelObjectView _playerView;
        private readonly List<LevelObjectView> _coinViews;

        public CoinsManager(LevelObjectView playerView, List<LevelObjectView> coinViews, SpriteAnimator spriteAnimator)
        {
            _playerView = playerView;
            _spriteAnimator = spriteAnimator;
            _coinViews = coinViews;
            _playerView.OnLevelObjectContact += OnLevelObjectContact;

            foreach (var coinView in coinViews)
            {
                _spriteAnimator.StartAnimation(coinView.SpriteRenderer, Track.Coin, true, Constants.Variables.ANIMATIONS_SPEED);
            }
        }
        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (_coinViews.Contains(contactView))
            {
                _spriteAnimator.StopAnimation(contactView.SpriteRenderer);
                contactView.gameObject.SetActive(false);
            }
        }
        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}