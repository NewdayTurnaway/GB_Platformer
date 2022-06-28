using UnityEngine.Tilemaps;

namespace GB_Platformer
{
    public abstract class LevelGeneratorController
    {
        private const int COUNT_WALL = 4;

        protected readonly Tilemap _tileMap;
        protected readonly TileBase _usedTile;
        protected readonly int _widthMap;
        protected readonly int _heightMap;
        protected readonly int _borderWidth;
        protected readonly int _factorSmooth;
        protected readonly int _randomFillPercent;

        protected readonly int[,] _map;

        public LevelGeneratorController(LevelGeneratorView generateLevelView)
        {
            _tileMap = generateLevelView.Tilemap;
            _usedTile = generateLevelView.UsedTile;
            _widthMap = generateLevelView.WidthMap;
            _heightMap = generateLevelView.HeightMap;
            _borderWidth = generateLevelView.BorderWidth;
            _factorSmooth = generateLevelView.FactorSmooth;
            _randomFillPercent = generateLevelView.RandomFillPercent;

            _map = new int[_widthMap, _heightMap];
        }

        public void Generate()
        {
            GenerateLevel();
        }

        public void ClearTileMap()
        {
            if (_tileMap != null)
            {
                _tileMap.ClearAllTiles();
            }
        }

        private void GenerateLevel()
        {
            RandomFillLevel();

            for (int i = 0; i < _factorSmooth; i++)
            {
                SmoothMap();
            }

            Draw();
        }

        protected abstract void Draw();

        private void RandomFillLevel()
        {
            System.Random pseudoRandom = new();

            for (int x = 0; x < _widthMap; x++)
            {
                for (int y = 0; y < _heightMap; y++)
                {
                    if (x <= _borderWidth - 1 || x >= _widthMap - _borderWidth || y <= _borderWidth - 1 || y >= _heightMap - _borderWidth)
                    {
                        _map[x, y] = 1;
                    }
                    else
                    {
                        _map[x, y] = (pseudoRandom.Next(0, 100) < _randomFillPercent) ? 1 : 0;
                    }
                }
            }
        }

        private void SmoothMap()
        {
            for (int x = 0; x < _widthMap; x++)
            {
                for (int y = 0; y < _heightMap; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallCount(x, y);
                    if (neighbourWallTiles > COUNT_WALL)
                    {
                        _map[x, y] = 1;
                    }
                    else if (neighbourWallTiles < COUNT_WALL)
                    {
                        _map[x, y] = 0;
                    }
                }
            }
        }

        private int GetSurroundingWallCount(int gridX, int gridY)
        {
            int wallCount = 0;
            for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            {
                for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
                {
                    if (neighbourX >= 0 && neighbourX < _widthMap && neighbourY >= 0 && neighbourY < _heightMap)
                    {
                        if (neighbourX != gridX || neighbourY != gridY)
                        {
                            wallCount += _map[neighbourX, neighbourY];
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }
            return wallCount;
        }
    }
}
