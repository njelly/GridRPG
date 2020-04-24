using Tofunaut.Core;
using Tofunaut.SharpUnity;

namespace Tofunaut.GridRPG.Game
{
    public class Tile : SharpGameObject
    {
        public readonly IntVector2 coord;
        public readonly Region region;

        public Tile(IntVector2 coord, Region region) : base($"{region.name}_{coord.ToString()}")
        {
            this.coord = coord;
            this.region = region;
        }
    }
}