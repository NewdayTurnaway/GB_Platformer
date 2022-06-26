using UnityEngine;

namespace GB_Platformer
{
    public sealed class MarchingSquares : LevelGeneratorController
    {
        private SquareGrid _squareGrid;

        public MarchingSquares(LevelGeneratorView generateLevelView) : base(generateLevelView)
        {
        }

        protected override void Draw()
        {
            _squareGrid = new SquareGrid(_map, 1);
            DrawTilesOnMap();
        }

        public void DrawTilesOnMap()
        {
            for (int x = 0; x < _squareGrid.Squares.GetLength(0); x++)
            {
                for (int y = 0; y < _squareGrid.Squares.GetLength(1); y++)
                {
                    DrawTileInControlNode(_squareGrid.Squares[x, y].TopLeft.Active, _squareGrid.Squares[x, y].TopLeft.Position);
                    DrawTileInControlNode(_squareGrid.Squares[x, y].TopRight.Active, _squareGrid.Squares[x, y].TopRight.Position);
                    DrawTileInControlNode(_squareGrid.Squares[x, y].BottomRight.Active, _squareGrid.Squares[x, y].BottomRight.Position);
                    DrawTileInControlNode(_squareGrid.Squares[x, y].BottomLeft.Active, _squareGrid.Squares[x, y].BottomLeft.Position);
                }
            }
        }

        private void DrawTileInControlNode(bool active, Vector3 position)
        {
            if (active)
            {
                Vector3Int positionTile = new((int)position.x, (int)position.y, 0);
                _tileMap.SetTile(positionTile, _usedTile);
            }
        }
    }
}
