using System;
using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    internal sealed class LevelObjectView : MonoBehaviour
    {
        public Action<LevelObjectView> OnLevelObjectContact { get; set; }
        public SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();
        public Transform Transform => transform;
        public Collider2D Collider2D => GetComponent<Collider2D>();
        public Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();

        void OnTriggerEnter2D(Collider2D collider)
        {
            LevelObjectView levelObject = collider.gameObject.GetComponent<LevelObjectView>();
            OnLevelObjectContact?.Invoke(levelObject);
        }
    } 
}
