using UnityEngine;

namespace GB_Platformer
{
    internal interface IProjectileBulletService
    {
        T InstantiateBullet<T>(BulletInfo bulletInfo);
        void Destroy(GameObject value);
    }
}