using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ParalaxEffect : ILateExecute
    {
        private readonly Camera _camera;
        private Vector3 _cameraStartPosition;

        private readonly SpriteRenderer _spriteRenderer;
        private readonly Transform _transform;
        private Vector3 _startTransformPosition;

        private readonly float _coefficient;
        private readonly bool _isInfinityHorizontal;
        private readonly bool _isInfinityVertical;
        private readonly float _textureUnitSizeX;
        private readonly float _textureUnitSizeY;

        public ParalaxEffect(Camera camera, SpriteRenderer spriteRenderer, float coefficient, 
            bool isInfinityHorizontal = false, bool isInfinityVertical = false)
        {
            _camera = camera;
            _cameraStartPosition = camera.transform.position;

            _spriteRenderer = spriteRenderer;
            _transform = _spriteRenderer.transform;
            _startTransformPosition = spriteRenderer.transform.position;
            
            _coefficient = coefficient;
            _isInfinityHorizontal = isInfinityHorizontal;
            _isInfinityVertical = isInfinityVertical;

            Sprite sprite = _spriteRenderer.sprite;

            _textureUnitSizeX = sprite.texture.width / sprite.pixelsPerUnit * _transform.localScale.x;
            _textureUnitSizeY = sprite.texture.height / sprite.pixelsPerUnit * _transform.localScale.y;
        }

        public void LateExecute()
        {
            _transform.position = _startTransformPosition + 
                ((_camera.transform.position - _cameraStartPosition) * _coefficient);

            if (_isInfinityHorizontal)
            {
                Infinity(true);
            }
            if (_isInfinityVertical)
            {
                Infinity(false);
            }
        }

        private void Infinity(bool isHorizontal)
        {
            float delta;
            float textureUnitSize;
            float xPosition = _transform.position.x;
            float yPosition = _transform.position.y;

            if (isHorizontal)
            {
                delta = _camera.transform.position.x - _transform.position.x;
                textureUnitSize = _textureUnitSizeX;
            }
            else
            {
                delta = _camera.transform.position.y - _transform.position.y;
                textureUnitSize = _textureUnitSizeY;
            }

            if (Mathf.Abs(delta) >= textureUnitSize)
            {
                float offsetPosition = (delta) % textureUnitSize;

                if (isHorizontal)
                {
                    xPosition = _camera.transform.position.x + offsetPosition;
                }
                else
                {
                    yPosition = _camera.transform.position.y + offsetPosition;
                }

                _transform.position = new(xPosition, yPosition);
                _cameraStartPosition = _camera.transform.position;
                _startTransformPosition = _transform.position;
            }
        }
    }
}