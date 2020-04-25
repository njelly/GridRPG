using Tofunaut.SharpUnity;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class TileView : SharpSprite
    {
        public TileView(Tile tile) : base($"TileView {tile.coord}", World.Sprites[tile.config.mainSpriteIndex])
        {

        }
    }
}