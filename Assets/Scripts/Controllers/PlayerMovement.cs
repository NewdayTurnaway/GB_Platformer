using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PlayerMovement : IExecute
    {
        private bool _isJump;
        private bool _isMove;
        private bool _onGround;

        private float _jumpVelocity;
        private float _inputHorizontal;

        private readonly PlayerInfo _characterView;
        private readonly SpriteAnimator _spriteAnimator;

        public PlayerMovement(PlayerInfo characterView, SpriteAnimator spriteAnimator)
        {
            _characterView = characterView;
            _spriteAnimator = spriteAnimator;
        }

        public void Execute()
        {
            _inputHorizontal = Input.GetAxis(Constants.Input.HORIZONTAL);

            CheckState();
            Move(_characterView.DoSomething, _isMove, _inputHorizontal);
            Jump(_onGround, _characterView.DoSomething);
        }

        private void CheckState()
        {
            _isJump = Input.GetAxis(Constants.Input.VERTICAL) > 0;
            _isMove = Mathf.Abs(_inputHorizontal) > _characterView.MovingTresh;
            _onGround = _characterView.PlayerSpriteRenderer.transform.position.y <= _characterView.GroundLevel && _jumpVelocity <= 0;
            _characterView.DoSomething = _spriteAnimator.IsNotLooped(_characterView.PlayerSpriteRenderer);
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

            _characterView.PlayerSpriteRenderer.transform.position += Vector3.right * (Time.deltaTime * _characterView.WalkSpeed * Mathf.Sign(inputHorizontal));
            _characterView.PlayerSpriteRenderer.flipX = inputHorizontal < 0;
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

            _spriteAnimator.StartAnimation(_characterView.PlayerSpriteRenderer, _isMove ? Track.Run : Track.Idle, true, _characterView.AnimationsSpeed);
            JumpState(_isJump);
        }

        private void JumpState(bool isJump)
        {
            if (isJump && Mathf.Approximately(_jumpVelocity, 0))
            {
                _jumpVelocity = _characterView.JumpStartSpeed;
            }
            else if (_jumpVelocity < 0)
            {
                _jumpVelocity = 0;
                SetInGroundLevel();
            }
        }

        private void Flying()
        {
            _jumpVelocity += _characterView.Acceleration * Time.deltaTime;

            if (Mathf.Abs(_jumpVelocity) > _characterView.FlyTresh)
            {
                _spriteAnimator.StartAnimation(_characterView.PlayerSpriteRenderer, Track.Jump, true, _characterView.AnimationsSpeed);
            }

            _characterView.PlayerSpriteRenderer.transform.position += Vector3.up * (Time.deltaTime * _jumpVelocity);
        }

        private void SetInGroundLevel()
        {
            _characterView.PlayerSpriteRenderer.transform.position = _characterView.PlayerSpriteRenderer.transform.position.Change(y: _characterView.GroundLevel);
        }
    }
}