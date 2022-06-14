using UnityEngine;

namespace GB_Platformer
{
    [System.Serializable]
    internal sealed class GameData
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private ParalaxInfo _paralaxInfo;
        [SerializeField] private PlayerInfo _playerInfo;
        [SerializeField] private CannonInfo _cannonInfo;
        
        public Camera Camera => _camera;
        public ParalaxInfo Paralax => _paralaxInfo;
        public PlayerInfo PlayerInfo => _playerInfo;
        public CannonInfo CannonInfo => _cannonInfo;
    }
}