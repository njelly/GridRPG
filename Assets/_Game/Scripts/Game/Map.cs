using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tofunaut.GridRPG.Game
{
    public class Map : MonoBehaviour
    {
        public Tilemap SolidTileMap => _solidTilemap;
        public MapPortal[] Portals => _portals;
        
        [SerializeField] private Tilemap _solidTilemap;
        [SerializeField] private MapPortal[] _portals;
    }
}