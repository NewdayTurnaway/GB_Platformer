namespace GB_Platformer
{
    internal sealed class PlayerController : IExecute, IFixedExecute
    {
        private readonly PlayerInfo _playerInfo;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly PlayerPhysicsMovement _playerPhysicsMovement;
        private readonly PlayerAttack _playerAttack;

        public PlayerController(PlayerInfo playerInfo, SpriteAnimator spriteAnimator)
        {
            _playerInfo = playerInfo;
            _spriteAnimator = spriteAnimator;
            _playerPhysicsMovement = new(_playerInfo, _spriteAnimator);
            _playerAttack = new(_playerInfo, _spriteAnimator);
        }

        public void Execute()
        {
            _playerAttack.Execute();
        }

        public void FixedExecute()
        {
            _playerPhysicsMovement.FixedExecute();
        }
    }
}