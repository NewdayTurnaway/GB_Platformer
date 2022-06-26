using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class EnemiesInfo
    {
        [NonReorderable]
        [SerializeField] private List<EnemyInfo> _enemyInfos;

        internal List<EnemyInfo> EnemyInfos => _enemyInfos;
    }
}