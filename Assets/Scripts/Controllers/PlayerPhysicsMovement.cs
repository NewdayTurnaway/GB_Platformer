using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PlayerPhysicsMovement : IFixedExecute
    {
        private bool _isJump;
        private bool _isMove;

        private float _inputHorizontal;
        private float _newVelocity;

        private readonly PlayerInfo _playerInfo;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly ContactsPoller _contactsPoller;

        public PlayerPhysicsMovement(PlayerInfo playerInfo, SpriteAnimator spriteAnimator)
        {
            _playerInfo = playerInfo;
            _spriteAnimator = spriteAnimator;
            _contactsPoller = new ContactsPoller(playerInfo.PlayerCollider2D);
        }

        public void FixedExecute()
        {
            _inputHorizontal = Input.GetAxis(Constants.Input.HORIZONTAL);

            CheckState();
            Move(_playerInfo.DoSomething, _isMove, _inputHorizontal);
            Jump(_contactsPoller.OnGround, _playerInfo.DoSomething);
        }

        private void CheckState()
        {
            _isJump = Input.GetAxis(Constants.Input.VERTICAL) > 0;
            _isMove = Mathf.Abs(_inputHorizontal) > _playerInfo.MovingTresh;
            _contactsPoller.Execute();
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
                _playerInfo.PlayerRigidbody2D.velocity = _playerInfo.PlayerRigidbody2D.velocity.Change(x: 0f);
                return;
            }

            SetVelocity(_inputHorizontal, Mathf.Lerp(0, _playerInfo.RunSpeed, Mathf.Abs(inputHorizontal)));
            _playerInfo.PlayerRigidbody2D.velocity = _playerInfo.PlayerRigidbody2D.velocity.Change(x: _newVelocity);
            _playerInfo.PlayerSpriteRenderer.flipX = inputHorizontal < 0;
        }

        private void SetVelocity(float inputHorizontal, float speed)
        {
            if ((inputHorizontal > 0 || !_contactsPoller.HasLeftContacts) && (inputHorizontal < 0 || !_contactsPoller.HasRightContacts))
            {
                _newVelocity = Time.fixedDeltaTime * speed * Mathf.Sign(inputHorizontal);
            }
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
            JumpAddForce(_isJump);
        }

        private void JumpAddForce(bool isJump)
        {
            if (isJump && Mathf.Abs(_playerInfo.PlayerRigidbody2D.velocity.y) <= _playerInfo.FlyTresh)
            {
                _playerInfo.PlayerRigidbody2D.AddForce(Vector2.up * _playerInfo.JumpForce);
            }
        }

        private void Flying()
        {
            if (Mathf.Abs(_playerInfo.PlayerRigidbody2D.velocity.y) > _playerInfo.FlyTresh)
            {
                _spriteAnimator.StartAnimation(_playerInfo.PlayerSpriteRenderer, Track.Jump, true, _playerInfo.AnimationsSpeed);
            }
        }
    }
}