using UnityEngine;

namespace GB_Platformer
{
    public static partial class BuilderExtensions
    {
        public static GameObject SetName(this GameObject gameObject, string name)
        {
            gameObject.name = name;
            return gameObject;
        }

        public static GameObject AddSprite(this GameObject gameObject, Sprite sprite)
        {
            SpriteRenderer component = gameObject.GetOrAddComponent<SpriteRenderer>();
            component.sprite = sprite;
            return gameObject;
        }

        public static GameObject AddSprite(this GameObject gameObject, Sprite sprite, Vector2 size)
        {
            SpriteRenderer component = gameObject.GetOrAddComponent<SpriteRenderer>();
            component.sprite = sprite;
            gameObject.transform.localScale = size;
            return gameObject;
        }

        public static GameObject AddRigidbody2D(this GameObject gameObject)
        {
            gameObject.GetOrAddComponent<Rigidbody2D>();
            return gameObject;
        }

        public static GameObject AddCircleCollider2D(this GameObject gameObject)
        {
            gameObject.GetOrAddComponent<CircleCollider2D>();
            return gameObject;
        }

        private static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T result = gameObject.GetComponent<T>();
            if (!result)
            {
                result = gameObject.AddComponent<T>();
            }
            return result;
        }
    }
}