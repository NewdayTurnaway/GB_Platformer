namespace GB_Platformer
{
    internal sealed class PlayerController : IExecute
    {
        private readonly PlayerInfo _playerInfo;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerAttack _playerAttack;


        public PlayerController(PlayerInfo playerInfo, SpriteAnimator spriteAnimator)
        {
            _playerInfo = playerInfo;
            _spriteAnimator = spriteAnimator;
            _playerMovement = new(_playerInfo, _spriteAnimator);
            _playerAttack = new(_playerInfo, _spriteAnimator);
        }

        public void Execute()
        {
            _playerMovement.Execute();
            _playerAttack.Execute();
        } 
    }
}