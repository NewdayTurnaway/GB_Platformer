using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    [CreateAssetMenu(fileName = "SpriteAnimationsData", menuName = "GameData/SpriteAnimationsData")]
    public sealed class SpriteAnimationsData : ScriptableObject
    {
        [SerializeField] private List<SpritesSequence> _sequences;

        internal List<SpritesSequence> Sequences => _sequences;
    }
}