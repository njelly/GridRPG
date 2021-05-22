using Tofunaut.GridRPG.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tofunaut.GridRPG.Game
{
    public class MapPortal : MonoBehaviour
    {
        public MapData ConnectedMap => _connectedMap;
        
        [SerializeField] private MapData _connectedMap;
    }
}