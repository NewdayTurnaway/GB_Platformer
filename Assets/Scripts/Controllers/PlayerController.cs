using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PlayerController : IExecute, IFixedExecute
    {
        private readonly PlayerInfo _playerInfo;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly PlayerPhysicsMovement _playerPhysicsMovement;
        private readonly PlayerAttack _playerAttack;
        private readonly PlayerApplyDamage _playerApplyDamage;

        public PlayerController(PlayerInfo playerInfo, SpriteAnimator spriteAnimator)
        {
            _playerInfo = playerInfo;
            _spriteAnimator = spriteAnimator;
            _playerPhysicsMovement = new(_playerInfo, _spriteAnimator);
            _playerAttack = new(_playerInfo, _spriteAnimator);
            _playerApplyDamage = new(_playerInfo, _spriteAnimator);
        }

        public void Execute()
        {
            _playerApplyDamage.Execute();

            if (_playerInfo.PlayerView.Death)
            {
                _playerInfo.PlayerRigidbody2D.velocity = Vector3.zero;
                return;
            }

            if (!_playerInfo.Abilities.Weapon)
            {
                return;
            }
            _playerAttack.Execute();
        }

        public void FixedExecute()
        {
            if (_playerInfo.PlayerView.Death)
            {
                _playerInfo.PlayerRigidbody2D.velocity = Vector3.zero;
                return;
            }
            _playerPhysicsMovement.FixedExecute();
        }
    }
}