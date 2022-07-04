namespace GB_Platformer
{
    public static class Constants
    {
        public struct Input
        {
            public const string HORIZONTAL = "Horizontal";
            public const string VERTICAL = "Vertical";
            public const string ATTACK1 = "Fire1";
            public const string ATTACK2 = "Fire2";
        }

        public struct Variables
        {
            public const float COLLISION_TRESH = 0.1f;
            public const float ANIMATIONS_SPEED = 10f;
            public const float DELAY_ATTACK = 2.5f;
            public const float ATTACK_DISTANCE = 0.25f;
            public const float POTION_HEATH_POINT = 20f;
            public const float MESSAGE_TIMER = 3f;
        }
        public struct Layer
        {
            public const string PLAYER = "Player";
            public const string ENEMY = "Enemy";
            public const string IGNORED = "Ignored";
        }
    }
}