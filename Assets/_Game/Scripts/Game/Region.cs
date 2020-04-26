using Tofunaut.Core;
using Tofunaut.SharpUnity;

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
        }
    }
}