using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class GameData
    {
        [Header("Game Settings")]
        [SerializeField] private Camera _camera;
        [SerializeField] private SpriteAnimationsData _spriteAnimationsData;
        [SerializeField] private List<LevelObjectView> _coins;
        [SerializeField] private List<LevelObjectView> _elevators;

        [Header("Objects Settings")]
        [SerializeField] private ParalaxInfo _paralaxInfo;
        [SerializeField] private PlayerInfo _playerInfo;
        [SerializeField] private CannonInfo _cannonInfo;
        
        public Camera Camera => _camera;
        public SpriteAnimationsData SpriteAnimationsData => _spriteAnimationsData;
        public List<LevelObjectView> Coins => _coins;
        public List<LevelObjectView> Elevators => _elevators;
        public ParalaxInfo Paralax => _paralaxInfo;
        public PlayerInfo PlayerInfo => _playerInfo;
        public CannonInfo CannonInfo => _cannonInfo;

    }
}