using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PatrolAIModel 
    {
        private readonly Transform[] _waypoints;
        private int _currentPointIndex;

        public PatrolAIModel(Transform[] waypoints)
        {
            _waypoints = waypoints;
            _currentPointIndex = 0;
        }

        public Transform GetNextTarget()
        {
            if (_waypoints == null)
            {
                return null;
            }

            _currentPointIndex = (_currentPointIndex + 1) % _waypoints.Length;
            return _waypoints[_currentPointIndex];
        }

        public Transform GetClosestTarget(Vector2 fromPosition)
        {
            if (_waypoints == null)
            {
                return null;
            }

            int closestIndex = 0;
            float closestDistance = 0.0f;

            for (int i = 0; i < _waypoints.Length; i++)
            {
                float distance = Vector2.Distance(fromPosition, _waypoints[i].position);
                
                if (closestDistance > distance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }
            _currentPointIndex = closestIndex;
            return _waypoints[_currentPointIndex];
        }
    }
}