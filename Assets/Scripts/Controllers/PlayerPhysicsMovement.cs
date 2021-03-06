using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PlayerPhysicsMovement : IFixedExecute
    {
        private bool _isJump;
        private bool _isMove;
        private bool _facingRight;

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
            _isMove = Mathf.Abs(_inputHorizontal) > _playerInfo.MovingThreshold;
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
            
            if ((inputHorizontal < 0 && !_facingRight) || (inputHorizontal > 0 && _facingRight))
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
                _playerInfo.InAir = true;
                Flying();
                return;
            }

            _playerInfo.InAir = false;

            if (doSomthing)
            {
                return;
            }

            _spriteAnimator.StartAnimation(_playerInfo.PlayerSpriteRenderer, _isMove ? Track.Run : Track.Idle, true, _playerInfo.AnimationsSpeed);

            if (!_playerInfo.Abilities.AbleJump)
            {
                return;
            }
            JumpAddForce(_isJump);
        }

        private void JumpAddForce(bool isJump)
        {
            if (isJump && Mathf.Abs(_playerInfo.PlayerRigidbody2D.velocity.y - _contactsPoller.GroundVelocity.y) <= _playerInfo.FlyThreshold)
            {
                _playerInfo.PlayerRigidbody2D.AddForce(Vector2.up * _playerInfo.JumpForce);
            }
        }

        private void Flying()
        {
            if (Mathf.Abs(_playerInfo.PlayerRigidbody2D.velocity.y) > _playerInfo.FlyThreshold)
            {
                _spriteAnimator.StartAnimation(_playerInfo.PlayerSpriteRenderer, Track.Jump, true, _playerInfo.AnimationsSpeed);
            }
        }
    }
}