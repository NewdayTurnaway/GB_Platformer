using System;
using System.Collections.Generic;

namespace GB_Platformer
{
    internal sealed class CoinsManager : IDisposable
    {
        private readonly SpriteAnimator _spriteAnimator;
        private readonly LevelObjectView _playerView;

        public CoinsManager(LevelObjectView playerView, List<CoinView> coinViews, SpriteAnimator spriteAnimator)
        {
            _playerView = playerView;
            _spriteAnimator = spriteAnimator;
            _playerView.OnLevelObjectContact += OnLevelObjectContact;

            foreach (LevelObjectView coinView in coinViews)
            {
                _spriteAnimator.StartAnimation(coinView.SpriteRenderer, Track.Coin, true, Constants.Variables.ANIMATIONS_SPEED);
            }
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (contactView is CoinView)
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