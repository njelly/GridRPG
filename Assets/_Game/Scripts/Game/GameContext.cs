using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class GameContext : MonoBehaviour
    {
        private static GameContext _instance;

        public static MapManager MapManager => _instance._mapManager;

        [SerializeField] private MapManager _mapManager;

        public void Awake()
        {
            if (_instance)
            {
                Debug.LogError("an instance of GameContext already exists");
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }
    }
}