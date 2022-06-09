using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ChangeColorGraduallyEffect : ILateExecute
    {
        private readonly Camera _camera;
        private Color _colorStart;
        private Color _colorEnd;
        private Color _colorCurrent;

        public ChangeColorGraduallyEffect(Camera camera, Color colorStart, Color colorEnd)
        {
            _camera = camera;
            _colorStart = colorStart;
            _colorEnd = colorEnd;
        }

        public void LateExecute()
        {
            _colorCurrent = Color.Lerp(_colorStart, _colorEnd, Mathf.PingPong(Time.time * 0.1f, 1));
            _camera.backgroundColor = _colorCurrent;
        }
    }
}