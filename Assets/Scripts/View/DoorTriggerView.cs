using System;
using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    internal sealed class DoorTriggerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _triggerZone;
        [SerializeField] private BoxCollider2D _boxCollider;

        public Action<GameObject> TriggerEnter;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public BoxCollider2D TriggerZone => _triggerZone;
        public BoxCollider2D BoxCollider => _boxCollider;

        private void OnValidate()
        {
            _spriteRenderer = _spriteRenderer != null ? _spriteRenderer : GetComponent<SpriteRenderer>();
            _triggerZone = _triggerZone != null ? _triggerZone : GetComponent<BoxCollider2D>();
            _triggerZone.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<PlayerView>())
            {
                TriggerEnter?.Invoke(other.gameObject);
            }
        }
    }
}