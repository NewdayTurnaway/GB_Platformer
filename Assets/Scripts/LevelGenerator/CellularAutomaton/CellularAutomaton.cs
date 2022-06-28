using UnityEngine;
using UnityEngine.Tilemaps;

namespace GB_Platformer
{
    public sealed class CellularAutomaton : LevelGeneratorController
    {
        public CellularAutomaton(LevelGeneratorView generateLevelView) : base(generateLevelView)
        {
        }

        protected override void Draw()
        {
            if (_map == null)
            {
                return;
            }

            for (int x = 0; x < _widthMap; x++)
            {
                for (int y = 0; y < _heightMap; y++)
                {
                    Vector3Int positionTile = new(-_widthMap / 2 + x, -_heightMap / 2 + y, 0);
                    if (_map[x, y] == 1)
                    {
                        _tileMap.SetTile(positionTile, _usedTile);
                    }
                }
            }
        }
    }
}
