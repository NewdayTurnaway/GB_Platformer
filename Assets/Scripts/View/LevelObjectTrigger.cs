using System;
using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(BoxCollider2D))]
    internal sealed class LevelObjectTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _boxCollider2D;

        public Action<GameObject> TriggerEnter;
        public Action<GameObject> TriggerExit;

        public BoxCollider2D BoxCollider2D => _boxCollider2D;

        private void OnValidate()
        {
            _boxCollider2D = _boxCollider2D != null ? _boxCollider2D : GetComponent<BoxCollider2D>();
            _boxCollider2D.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<PlayerView>())
            {
                TriggerEnter?.Invoke(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerView>())
            {
                TriggerExit?.Invoke(other.gameObject);
            }
        }
    }
}