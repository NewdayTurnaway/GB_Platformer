using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ContactsPoller : IExecute
    {
        private readonly ContactPoint2D[] _contacts = new ContactPoint2D[8];
        private readonly Collider2D _collider2D;

        public bool OnGround { get; private set; }
        public bool HasLeftContacts { get; private set; }
        public bool HasRightContacts { get; private set; }

        public ContactsPoller(Collider2D collider2D)
        {
            _collider2D = collider2D;
        }

        public void Execute()
        {
            OnGround = false;
            HasLeftContacts = false;
            HasRightContacts = false;

            for (int i = 0; i < _collider2D.GetContacts(_contacts); i++)
            {
                Vector2 normal = _contacts[i].normal;
                Rigidbody2D rigidbody2D = _contacts[i].rigidbody;

                if (normal.y > Constants.Variables.COLLISION_TRESH)
                {
                    OnGround = true;
                }

                if (normal.x > Constants.Variables.COLLISION_TRESH && rigidbody2D == null)
                {
                    HasLeftContacts = true;
                }

                if (normal.x < -Constants.Variables.COLLISION_TRESH && rigidbody2D == null)
                {
                    HasRightContacts = true;
                }
            }
        }
    }
}