using Tofunaut.GridRPG.Game.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tofunaut.GridRPG.Game
{
    public class GameContext : MonoBehaviour
    {
        public static GameCameraController GameCamera => _instance._gameCameraController;
        public static MapManager MapManager => _instance._mapManager;
        public static DialogView DialogView => _instance._dialogView;
        public static PlayerInput PlayerInput => _instance._playerInput;
        public static Actor PlayerActor { get; set; }
        
        private static GameContext _instance;
        
        [SerializeField] private GameCameraController _gameCameraController;
        [SerializeField] private PlayerActorInputProvider _playerActorInputProvider;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private DialogView _dialogView;

        private MapManager _mapManager;
        private Actor _playerActorInputTarget;

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

        public static void SetPlayerActorInputTarget(Actor actor)
        {
            if(_instance._playerActorInputTarget)
                _instance._playerActorInputTarget.SetActorInputProviderOverride(null);
            
            _instance._playerActorInputTarget = actor;
            
            if(_instance._playerActorInputTarget)
                _instance._playerActorInputTarget.SetActorInputProviderOverride(_instance._playerActorInputProvider);
        }
    }
}