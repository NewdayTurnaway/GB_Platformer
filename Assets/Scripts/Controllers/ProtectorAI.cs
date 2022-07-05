using Pathfinding;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ProtectorAI : IProtector, IInitialization, IFixedExecute, IDeinitialization
    {
        private readonly Transform _targetTransform;
        private readonly Transform _protectorTransform;
        private readonly Transform _targetWaypoint;
        private readonly Transform _attackPointTransform;
        private readonly EnemyInfo _enemyInfo;
        private readonly EnemyType _enemyType;
        private readonly PatrolAIModel _model;
        private readonly AIDestinationSetter _destinationSetter;
        private readonly AIPatrolPath _patrolPath;

        private bool _isPatrolling;

        public ProtectorAI(Transform targetTransform, EnemyView enemyView, EnemyInfo enemyInfo, EnemyType enemyType, PatrolAIModel model)
        {
            _targetTransform = targetTransform;
            _protectorTransform = enemyView.Transform;
            _targetWaypoint = enemyView.TargetWaypoint;
            _attackPointTransform = enemyView.AttackPointTransform;
            _enemyInfo = enemyInfo;
            _enemyType = enemyType;
            _model = model;
            _destinationSetter = enemyView.ProtectorAIDestinationSetter;
            _patrolPath = enemyView.ProtectorAIPatrolPath;
        }

        public void Initialization()
        {
            _destinationSetter.target = CorrectionWaypoint(_model.GetNextTarget());
            _isPatrolling = true;
            _patrolPath.TargetReached += OnTargetReached;
        }

        public void FixedExecute()
        {
            if (!_isPatrolling)
            {
                _targetWaypoint.position = _targetTransform.position;

                float distance = Vector2.Distance(_targetWaypoint.position, _attackPointTransform.position);
                _enemyInfo.InAttackDistance = distance < Constants.Variables.DELAY_ATTACK_DISTANCE;

                if (_enemyType == EnemyType.Patrol)
                {
                    Vector3 position = _targetWaypoint.position;
                    position.Set(position.x, _protectorTransform.position.y, position.z);
                    _targetWaypoint.position = position;
                }
                return;
            }
            _enemyInfo.InAttackDistance = false;
        }

        private Transform CorrectionWaypoint(Transform transform)
        {
            Transform target = transform;
            if (_enemyType == EnemyType.Patrol)
            {
                Vector3 position = transform.position;
                position.Set(position.x, _protectorTransform.position.y, position.z);
                target.position = position;
                return target;
            }
            return target;
        }

        public void Deinitialization()
        {
            _patrolPath.TargetReached -= OnTargetReached;
        }

        private void OnTargetReached()
        {
            _destinationSetter.target = _isPatrolling ? CorrectionWaypoint(_model.GetNextTarget()) : _targetWaypoint;
        }

        public void StartProtection(GameObject invader)
        {
            _isPatrolling = false;
            _destinationSetter.target = _targetWaypoint;
        }

        public void FinishProtection(GameObject invader)
        {
            _isPatrolling = true;
            _targetWaypoint.localPosition = Vector3.zero;
            _destinationSetter.target = CorrectionWaypoint(_model.GetClosestTarget(_targetWaypoint.position));
        }

        public void ResetPosition()
        {
            Transform WaypointTransform = CorrectionWaypoint(_model.GetNextTarget());
            _protectorTransform.position = WaypointTransform.position;
            _destinationSetter.target = WaypointTransform;
            _isPatrolling = true;
        }
    }
}