using System;
using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class ProjectileService : IProjectileService, IProjectileBulletService
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

        public T InstantiateBullet<T>(BulletInfo bulletInfo)
        {
            if (!_viewCache.TryGetValue(Constants.Name.BULLET_NAME, out ObjectPool viewPool))
            {
                viewPool = new ObjectPool(bulletInfo.Sprite, Vector2.zero.Change(x: bulletInfo.SpriteSize, y: bulletInfo.SpriteSize));
                _viewCache[Constants.Name.BULLET_NAME] = viewPool;
            }

            return viewPool.Pop().TryGetComponent(out T component) ? component : throw new InvalidOperationException($"{typeof(T)} not found");
        }

        public void Destroy(GameObject value)
        {
            _viewCache[value.name].Push(value);
        }
    }
}