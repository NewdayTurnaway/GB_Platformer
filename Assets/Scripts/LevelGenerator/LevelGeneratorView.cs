using UnityEngine;
using UnityEngine.Tilemaps;

namespace GB_Platformer
{
    public sealed class LevelGeneratorView : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private TileBase _tileBase;
        [SerializeField] private int _widthMap;
        [SerializeField] private int _heightMap;
        [SerializeField] private int _borderWidth;
        [SerializeField] private int _factorSmooth;
        [SerializeField, Range(0, 100)] private int _randomFillPercent;

        public Tilemap Tilemap => _tilemap;
        public TileBase UsedTile => _tileBase;
        public int WidthMap => _widthMap > 0 ? _widthMap : 1;
        public int HeightMap => _heightMap > 0 ? _heightMap : 1;
        public int BorderWidth => _borderWidth >0 ? _borderWidth : 1;
        public int FactorSmooth => _factorSmooth > 0 ? _factorSmooth : 1;
        public int RandomFillPercent => _randomFillPercent;
    } 
}
