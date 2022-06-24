using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class EnemyInfo
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;
        [SerializeField] private float _speed;
        [SerializeField] private float _minDistanceToTarget;
        [SerializeField] private Transform[] _waypoints;

        public EnemyType EnemyType => _enemyType;
        public EnemyView EnemyView => _enemyView;
        public LevelObjectTrigger ProtectedZoneTrigger => _protectedZoneTrigger;
        public float Speed => _speed;
        public float MinDistanceToTarget => _minDistanceToTarget;
        public Transform[] Waypoints => _waypoints;

    }
}