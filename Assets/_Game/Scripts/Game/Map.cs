using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tofunaut.GridRPG.Game
{
    public class Map : MonoBehaviour
    {
        public MapPortal[] Portals => _portals;
        
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private MapPortal[] _portals;
    }
}