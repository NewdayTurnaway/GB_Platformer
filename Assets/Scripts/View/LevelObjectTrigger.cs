using System;
using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(BoxCollider2D))]
    internal sealed class LevelObjectTrigger : MonoBehaviour
    {
        public Action<GameObject> TriggerEnter;
        public Action<GameObject> TriggerExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.PLAYER))
            {
                TriggerEnter?.Invoke(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.PLAYER))
            {
                TriggerExit?.Invoke(other.gameObject);
            }
        }
    }
}