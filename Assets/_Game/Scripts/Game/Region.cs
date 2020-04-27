using System;
using Tofunaut.Core;
using Tofunaut.SharpUnity;
using Tofunaut.UnityUtils;

namespace Tofunaut.GridRPG.Game
{
    public class Region : SharpGameObject
    {
        public readonly World world;
        public readonly IntVector2 coord;

        private readonly RegionState _state;

        public Region(World world, RegionState state, IntVector2 coord) : base($"Region {coord.ToString()}")
        {
            this.world = world;
            this.coord = coord;

            _state = state;

            for (int x = 0; x < _state.tiles.GetLength(0); x++)
            {
                for (int y = 0; y < _state.tiles.GetLength(1); y++)
                {
                    IntVector2 tileCoord = new IntVector2(x, y);
                    Tile tile = new Tile(this, _state.tiles[x, y], new IntVector2(x, y));
                    tile.LocalPosition = tileCoord.ToUnityVector3_XY();
                    AddChild(tile);
                }
            }
        }
    }
}