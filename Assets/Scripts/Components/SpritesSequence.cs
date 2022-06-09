using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class SpritesSequence
    {
        public Track Track;
        public List<Sprite> Sprites = new();
    }
}