using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PlayerView : LevelObjectView
    {
        [SerializeField] private Health _health;
        [SerializeField] private Transform _headTransform;
        [SerializeField] private Transform _attackPointTransform;
        [SerializeField] private float _attackRange = 0.85f;

        public Health Health => _health;
        public Transform HeadTransform => _headTransform;
        public Transform AttackPointTransform => _attackPointTransform;
        public float AttackRange => _attackRange;

        protected override void OnValidate()
        {
            base.OnValidate();

            _health = new(_health.MaxHealth);

            if (_transform.childCount >= 2)
            {
                _headTransform = _headTransform != null ? _headTransform : _transform.GetChild(0).transform;
                _attackPointTransform = _attackPointTransform != null ? _attackPointTransform : _transform.GetChild(1).transform;
            }
        }

        public void TakeDamage(float damage)
        {
            Health.CurrentHealth -= damage;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(AttackPointTransform.position, AttackRange);
        }
    } 
}
