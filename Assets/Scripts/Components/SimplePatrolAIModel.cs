using UnityEngine;

namespace GB_Platformer
{
    internal sealed class SimplePatrolAIModel
    {
        private readonly EnemyInfo _enemyInfo;
        private Transform _target;
        private int _currentPointIndex;

        public SimplePatrolAIModel(EnemyInfo enemyInfo)
        {
            _enemyInfo = enemyInfo;
            _target = GetNextWaypoint();
        }

        public Vector2 CalculateVelocity(Vector2 fromPosition)
        {
            Vector2 direction = (Vector2)_target.position - fromPosition;
            _target = direction.magnitude <= _enemyInfo.MinDistanceToTarget ? GetNextWaypoint() : _target;
            return _enemyInfo.Speed * direction.normalized;
        }

        private Transform GetNextWaypoint()
        {
            _currentPointIndex = (_currentPointIndex + 1) % _enemyInfo.Waypoints.Length;
            return _enemyInfo.Waypoints[_currentPointIndex];
        }
    }
}