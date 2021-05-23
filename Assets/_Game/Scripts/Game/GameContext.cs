using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class GameContext : MonoBehaviour
    {
        public static GameCameraController GameCamera => _instance._gameCameraController;
        public static MapManager MapManager => _instance._mapManager;
        public static PlayerActorInputProvider PlayerActorInputProvider => _instance._playerActorInputProvider;
        
        private static GameContext _instance;
        
        [SerializeField] private GameCameraController _gameCameraController;
        [SerializeField] private PlayerActorInputProvider _playerActorInputProvider;

        private MapManager _mapManager;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            _mapManager = new MapManager();
        }
    }
}