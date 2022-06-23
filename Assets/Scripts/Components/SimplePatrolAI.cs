using UnityEngine;

namespace GB_Platformer
{
    internal sealed class SimplePatrolAI : IFixedExecute
    {
        private readonly EnemyView _view;
        private readonly SimplePatrolAIModel _model;

        public SimplePatrolAI(EnemyView view, SimplePatrolAIModel model)
        {
            _view = view;
            _model = model;
        }

        public void FixedExecute()
        {
            _view.Rigidbody2D.velocity = _model.CalculateVelocity(_view.transform.position) * Time.fixedDeltaTime;
        }
    }
}