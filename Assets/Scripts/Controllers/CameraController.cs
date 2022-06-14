using UnityEngine;

namespace GB_Platformer
{
    internal sealed class CameraController : IExecute
    {
        private readonly Transform _targetTransform;
        private Vector3 _offsetCamera;
        private readonly Transform _camTransform;

        public CameraController(Transform cameraTransform, Transform targetTransform)
        {
            _targetTransform = targetTransform;
            _camTransform = cameraTransform;
            _offsetCamera = _camTransform.position - _targetTransform.position;
        }

        public void Execute()
        {
            _camTransform.position = _targetTransform.position + _offsetCamera;
        }
    }
}