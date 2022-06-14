using UnityEngine;

namespace GB_Platformer
{
    public interface IProjectileService
    {
        T Instantiate<T>(GameObject prefab);
        void Destroy(GameObject value);
    }
}