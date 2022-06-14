using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class ParalaxInfo 
    {
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private float _backCoefficient;
        [SerializeField] private SpriteRenderer _midground;
        [SerializeField] private float _midCoefficient;
        [SerializeField] private SpriteRenderer _foreground;
        [SerializeField] private float _foreCoefficient;
        [SerializeField] private Color _backgroundColor1;
        [SerializeField] private Color _backgroundColor2;

        public SpriteRenderer Background => _background;
        public float BackCoefficient => _backCoefficient;
        public SpriteRenderer Midground => _midground;
        public float MidCoefficient => _midCoefficient;
        public SpriteRenderer Foreground => _foreground;
        public float ForeCoefficient => _foreCoefficient;
        public Color BackgroundColor1 => _backgroundColor1;
        public Color BackgroundColor2 => _backgroundColor2;
    }
}