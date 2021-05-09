using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tofunaut.GridRPG.Game
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private MapPortal _portals;
    }
}