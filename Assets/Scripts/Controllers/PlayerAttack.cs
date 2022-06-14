using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PlayerAttack : IExecute
    {
        private readonly PlayerInfo _characterView;
        private readonly SpriteAnimator _spriteAnimator;

        public PlayerAttack(PlayerInfo characterView, SpriteAnimator spriteAnimator)
        {
            _characterView = characterView;
            _spriteAnimator = spriteAnimator;
        }

        public void Execute()
        {
            Attack(Constants.Input.ATTACK1, Track.Attack1);
            Attack(Constants.Input.ATTACK2, Track.Attack2);
        }

        private void Attack(string inputButton, Track track)
        {
            if (Input.GetButtonDown(inputButton))
            {
                _spriteAnimator.StartAnimation(_characterView.PlayerSpriteRenderer, track, false, _characterView.AnimationsSpeed);
            }
        }
    }
}