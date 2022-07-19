using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PlayerMovement : IExecute
    {
        private bool _isJump;
        private bool _isMove;
        private bool _onGround;
        private bool _facingRight;

        private float _jumpVelocity;
        private float _inputHorizontal;

        private readonly PlayerInfo _playerInfo;
        private readonly SpriteAnimator _spriteAnimator;

        public PlayerMovement(PlayerInfo playerInfo, SpriteAnimator spriteAnimator)
        {
            _playerInfo = playerInfo;
            _spriteAnimator = spriteAnimator;
        }

        public void Execute()
        {
            _inputHorizontal = Input.GetAxis(Constants.Input.HORIZONTAL);

            CheckState();
            Move(_playerInfo.DoSomething, _isMove, _inputHorizontal);
            Jump(_onGround, _playerInfo.DoSomething);
        }

        private void CheckState()
        {
            _isJump = Input.GetAxis(Constants.Input.VERTICAL) > 0;
            _isMove = Mathf.Abs(_inputHorizontal) > _playerInfo.MovingThreshold;
            _onGround = _playerInfo.PlayerSpriteRenderer.transform.position.y <= _playerInfo.GroundLevel && _jumpVelocity <= 0;
            _playerInfo.DoSomething = _spriteAnimator.IsNotLooped(_playerInfo.PlayerSpriteRenderer);
        }

        private void Move(bool doSomthing, bool isMove, float inputHorizontal)
        {
            if (doSomthing)
            {
                return;
            }

            if (!isMove)
            {
                return;
            }

            _playerInfo.PlayerSpriteRenderer.transform.position += Vector3.right * (Time.deltaTime * _playerInfo.RunSpeed * Mathf.Sign(inputHorizontal));
            if (inputHorizontal < 0 && !_facingRight)
            {
                Flip();
            }
            if (inputHorizontal > 0 && _facingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            Vector3 newVector = _playerInfo.PlayerView.Transform.localScale;
            newVector.x *= -1;
            _playerInfo.PlayerView.Transform.localScale = newVector;

            _facingRight = !_facingRight;
        }

        private void Jump(bool onGround, bool doSomthing)
        {
            if (!onGround)
            {
                Flying();
                return;
            }

            if (doSomthing)
            {
                return;
            }

            _spriteAnimator.StartAnimation(_playerInfo.PlayerSpriteRenderer, _isMove ? Track.Run : Track.Idle, true, _playerInfo.AnimationsSpeed);
            JumpState(_isJump);
        }

        private void JumpState(bool isJump)
        {
            if (isJump && Mathf.Approximately(_jumpVelocity, 0))
            {
                _jumpVelocity = _playerInfo.JumpForce;
            }
            else if (_jumpVelocity < 0)
            {
                _jumpVelocity = 0;
                SetInGroundLevel();
            }
        }

        private void Flying()
        {
            _jumpVelocity += _playerInfo.Acceleration * Time.deltaTime;

            if (Mathf.Abs(_jumpVelocity) > _playerInfo.FlyThreshold)
            {
                _spriteAnimator.StartAnimation(_playerInfo.PlayerSpriteRenderer, Track.Jump, true, _playerInfo.AnimationsSpeed);
            }

            _playerInfo.PlayerSpriteRenderer.transform.position += Vector3.up * (Time.deltaTime * _jumpVelocity);
        }

        private void SetInGroundLevel()
        {
            _playerInfo.PlayerSpriteRenderer.transform.position = _playerInfo.PlayerSpriteRenderer.transform.position.Change(y: _playerInfo.GroundLevel);
        }
    }
}