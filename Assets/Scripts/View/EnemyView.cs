﻿using UnityEngine;

namespace GB_Platformer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    internal sealed class EnemyView : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();
        public Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();
        public Collider2D Collider2D => GetComponent<Collider2D>();
    }
}