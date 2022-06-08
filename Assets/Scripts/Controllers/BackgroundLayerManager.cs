using UnityEngine;

namespace GB_Platformer
{
    internal sealed class BackgroundLayerManager : ILateExecute
    {
        private readonly Camera _camera;

        private readonly SpriteRenderer  _backSpriteRenderer;
        private readonly SpriteRenderer _midSpriteRenderer;
        private readonly SpriteRenderer _foreSpriteRenderer;

        private readonly float _backCoefficient;
        private readonly float _midCoefficient;
        private readonly float _foreCoefficient;

        private readonly ParalaxEffect _backParalax;
        private readonly ParalaxEffect _midParalax;
        private readonly ParalaxEffect _foreParalax;

        private readonly ChangeColorGraduallyEffect _changeColorGraduallyEffect;
        private Color _colorStart;
        private Color _colorEnd;

        public BackgroundLayerManager(Camera camera, SpriteRenderer backSpriteRenderer, float backCoefficient,
            SpriteRenderer midSpriteRenderer, float midCoefficient,
            SpriteRenderer foreSpriteRenderer, float foreCoefficient, Color? color1 = null, Color? color2 = null)
        {
            _camera = camera;
            _colorStart = color1 ?? default;
            _colorEnd = color2 ?? default;

            _backSpriteRenderer = backSpriteRenderer;
            _backCoefficient = backCoefficient;
            _changeColorGraduallyEffect = new(_camera, _colorStart, _colorEnd);
            _backParalax = new(_camera, _backSpriteRenderer, _backCoefficient, true, true);

            _midSpriteRenderer = midSpriteRenderer;
            _midCoefficient = midCoefficient;
            _midParalax = new(_camera, _midSpriteRenderer, _midCoefficient, true, true);

            _foreSpriteRenderer = foreSpriteRenderer;
            _foreCoefficient = foreCoefficient;
            _foreParalax = new(_camera, _foreSpriteRenderer, _foreCoefficient, true);
        }

        public void LateExecute()
        {
            _changeColorGraduallyEffect.LateExecute();
            _backParalax.LateExecute();
            _midParalax.LateExecute();
            _foreParalax.LateExecute();
        }
    }
}