using Tofunaut.Core;
using Tofunaut.SharpUnity;

namespace Tofunaut.GridRPG.Game
{
    public class Tile : SharpGameObject
    {
        public IntVector2 WorldCoord => new IntVector2(RegionState.Size * region.coord.x + coord.x, RegionState.Size * region.coord.y + coord.y);

        public readonly Region region;
        public readonly IntVector2 coord;

        private readonly TileState _state;
        private readonly TileView _view;

        public Tile(Region region, TileState state, IntVector2 coord) : base($"{region.Name}, Tile {coord.ToString()}")
        {
            this.region = region;
            this.coord = coord;

            _state = state;

            _view = new TileView(this, SpriteAtlasManager.Get(1));
            AddChild(_view);
        }
    }
}