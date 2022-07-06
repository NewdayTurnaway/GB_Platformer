using UnityEngine;

namespace GB_Platformer
{
    public sealed class GameRoot : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;

        private Controllers _controllers;

        public GameData GameData { get => _gameData; set => _gameData = value; }

        private void Awake()
        {
            _controllers = new();
            new GameInitialization(_controllers, _gameData);
            _controllers.Initialization();
        }
        
        private void Update()
        {
            _controllers.Execute();
        }

        private void LateUpdate()
        {
            _controllers.LateExecute();
        }

        private void FixedUpdate()
        {
            _controllers.FixedExecute();
        }

        private void OnDestroy()
        {
            _controllers.Deinitialization();
        }
    }
}