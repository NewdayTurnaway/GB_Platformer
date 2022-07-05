using System;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class PlayerView : LevelObjectView
    {
        [SerializeField] private Health _health;
        [SerializeField] private Transform _headTransform;
        [SerializeField] private Transform _attackPointTransform;
        [SerializeField] private float _attackRange = 0.85f;

        private float _coinsCounter;
        private bool _death;


        public Health Health => _health;
        public Action ChangeHeath;
        public Action ChangeCoinsCounter;
        public Transform HeadTransform => _headTransform;
        public Transform AttackPointTransform => _attackPointTransform;
        public float AttackRange => _attackRange;
        public float CoinsCounter => _coinsCounter;
        public bool Death { get => _death; set => _death = value; }

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

        public void ResetHeath()
        {
            Health.CurrentHealth = Health.MaxHealth;
            ChangeHeath?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            Health.CurrentHealth -= damage;
            if (Mathf.Approximately(Health.CurrentHealth, 0f))
            {
                Death = true;
            }
            ChangeHeath?.Invoke();
        }

        public void AddCoin()
        {
            _coinsCounter++;
            ChangeCoinsCounter?.Invoke();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(AttackPointTransform.position, AttackRange);
        }
    } 
}
