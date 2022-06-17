using System;
using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ProjectileService : IProjectileService
    {
        private readonly Dictionary<string, ObjectPool> _viewCache = new(12);

        public T Instantiate<T>(GameObject prefab)
        {
            if (!_viewCache.TryGetValue(prefab.name, out ObjectPool viewPool))
            {
                viewPool = new ObjectPool(prefab);
                _viewCache[prefab.name] = viewPool;
            }

            return viewPool.Pop().TryGetComponent(out T component) ? component : throw new InvalidOperationException($"{typeof(T)} not found");
        }

        public void Destroy(GameObject value)
        {
            _viewCache[value.name].Push(value);
        }
    }
}