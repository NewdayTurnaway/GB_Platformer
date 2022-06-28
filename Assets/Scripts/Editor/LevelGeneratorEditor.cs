using UnityEngine;
using UnityEditor;

namespace GB_Platformer
{
    [CustomEditor(typeof(LevelGeneratorView))]
    public class LevelGeneratorEditor : Editor
    {
        private CellularAutomaton _cellularAutomaton;
        private MarchingSquares _marchingSquares;
        private LevelGeneratorType _levelGeneratorType;

        private void OnEnable()
        {
            LevelGeneratorView levelGeneratorView = (LevelGeneratorView)target;
            _cellularAutomaton = new(levelGeneratorView);
            _marchingSquares = new(levelGeneratorView);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.HelpBox("If the Width/Height/Border of the Map < 0 then = 1", MessageType.Info);

            _levelGeneratorType = (LevelGeneratorType)EditorGUILayout.EnumPopup(_levelGeneratorType);

            if (GUILayout.Button("Generate"))
            {
                switch (_levelGeneratorType)
                {
                    case LevelGeneratorType.CellularAutomaton:
                        _cellularAutomaton = new((LevelGeneratorView)target);
                        _cellularAutomaton.Generate();
                        break;
                    case LevelGeneratorType.MarchingSquares:
                        _marchingSquares = new((LevelGeneratorView)target);
                        _marchingSquares.Generate();
                        break;
                }
            }

            if (GUILayout.Button("Clear"))
            {
                switch (_levelGeneratorType)
                {
                    case LevelGeneratorType.CellularAutomaton:
                        _cellularAutomaton = new((LevelGeneratorView)target);
                        _cellularAutomaton.ClearTileMap();
                        break;
                    case LevelGeneratorType.MarchingSquares:
                        _marchingSquares = new((LevelGeneratorView)target);
                        _marchingSquares.ClearTileMap();
                        break;
                }
            }
        }
    }
}
