using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class GameData
    {
        [SerializeField] private Camera _camera;

        [Header("Paralax")]
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private float _backCoefficient;
        [SerializeField] private SpriteRenderer _midground;
        [SerializeField] private float _midCoefficient;
        [SerializeField] private SpriteRenderer _foreground;
        [SerializeField] private float _foreCoefficient;
        [SerializeField] private Color _backgroundColor1;
        [SerializeField] private Color _backgroundColor2;

        [Header("Player")]
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;
        [SerializeField] private SpriteAnimationsData _playerAnimationsData;

        public Camera Camera => _camera;
        public SpriteRenderer Background => _background;
        public float BackCoefficient => _backCoefficient;
        public SpriteRenderer Midground => _midground;
        public float MidCoefficient => _midCoefficient;
        public SpriteRenderer Foreground => _foreground;
        public float ForeCoefficient => _foreCoefficient;
        public Color BackgroundColor1 => _backgroundColor1;
        public Color BackgroundColor2 => _backgroundColor2;
        public SpriteRenderer PlayerSpriteRenderer => _playerSpriteRenderer;
        public SpriteAnimationsData PlayerAnimationsData => _playerAnimationsData;
    }
}