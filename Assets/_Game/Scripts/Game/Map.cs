using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tofunaut.GridRPG.Game
{
    public class Map : MonoBehaviour
    {
        public Tilemap SolidTileMap => _solidTilemap;
        public MapPortal[] Portals => _portals;
        public PolygonCollider2D CameraBounds => _cameraBounds;
        
        [SerializeField] private Tilemap _solidTilemap;
        [SerializeField] private MapPortal[] _portals;
        [SerializeField] private PolygonCollider2D _cameraBounds;
    }
}