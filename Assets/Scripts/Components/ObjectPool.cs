using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GB_Platformer
{
    internal sealed class ObjectPool : IDisposable
    {
        private readonly Stack<GameObject> _stack = new();
        private readonly GameObject _prefab;
        private readonly Transform _root;

        public ObjectPool(GameObject prefab)
        {
            _prefab = prefab;
            _root = new GameObject().SetName($"[{_prefab.name}]").transform;
        }

        public ObjectPool(Sprite sprite, Vector2 size)
        {
            _prefab = new GameObject().SetName(Constants.Name.BULLET_NAME).AddSprite(sprite, size);
            _root = new GameObject().SetName($"[{_prefab.name}]").transform;
            Push(_prefab);
        }

        public GameObject Pop()
        {
            GameObject gameObject;
            if (_stack.Count == 0)
            {
                gameObject = Object.Instantiate(_prefab).SetName(_prefab.name);
            }
            else
            {
                gameObject = _stack.Pop();
            }
            gameObject.SetActive(true);
            gameObject.transform.SetParent(null);
            return gameObject;
        }

        public void Push(GameObject gameObject)
        {
            _stack.Push(gameObject);
            gameObject.transform.SetParent(_root);
            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            for (int i = 0; i < _stack.Count; i++)
            {
                GameObject gameObject = _stack.Pop();
                Object.Destroy(gameObject);
            }
            Object.Destroy(_root.gameObject);
        }
    }
}