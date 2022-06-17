using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class BulletInfo
    {
        [SerializeField] private GameObject _prefab;

        [Header("Settings")]
        [SerializeField] private float _delay = 1;
        [SerializeField] private float _timeDestroy = 3;
        [SerializeField] private float _force = 5;
        [SerializeField] private float _radius = 0.03f;
        [SerializeField] private float _groundLevel = 0;
        [SerializeField] private float _acceleration = -10;

        public GameObject Prefab => _prefab;
        public float Radius => _radius;
        public float Delay => _delay;
        public float TimeDestroy => _timeDestroy;
        public float Force => _force;
        public float GroundLevel => _groundLevel;
        public float Acceleration => _acceleration;
    }
}