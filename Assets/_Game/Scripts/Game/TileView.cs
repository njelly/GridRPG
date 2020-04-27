using Tofunaut.SharpUnity;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class TileView : SharpSprite
    {
        public TileView(Tile tile, Sprite sprite) : base($"{tile.Name}_View", sprite)
        {

        }
    }
}