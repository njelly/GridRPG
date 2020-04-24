using Tofunaut.Core;
using Tofunaut.SharpUnity;

namespace Tofunaut.GridRPG.Game
{
    public class Region : SharpGameObject
    {
        public IntVector2 RegionSize = IntVector2.One * 256;

        public readonly IntVector2 coord;

        private Region _northRegion;
        private Region _southRegion;
        private Region _eastRegion;
        private Region _westRegion;

        public Region(World world, IntVector2 coord) : base($"Region {coord.ToString()}")
        {
            this.coord = coord;
        }
    }
}