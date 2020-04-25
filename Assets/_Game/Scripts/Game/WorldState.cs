using System;
using System.Collections.Generic;
using Tofunaut.Core;

namespace Tofunaut.GridRPG.Game
{
    [Serializable]
    public class WorldState
    {
        public int seed;
        public IntVector2 playerRegion;
        public Dictionary<int, Dictionary<int, RegionState>> regions;

        public WorldState(int seed)
        {
            this.seed = seed;
            playerRegion = IntVector2.Zero;
            regions = new Dictionary<int, Dictionary<int, RegionState>>();
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}