using System;
using System.Collections.Generic;
using Tofunaut.Core;

namespace Tofunaut.GridRPG.Game
{
    [Serializable]
    public class WorldState
    {
        public uint InvalidActorId = 0;

        public int seed;
        public Dictionary<int, Dictionary<int, RegionState>> regions;
        public PlayerState playerState;
        public uint actorIdCounter;

        public WorldState(int seed)
        {
            this.seed = seed;
            regions = new Dictionary<int, Dictionary<int, RegionState>>();
            playerState = new PlayerState();
            actorIdCounter = InvalidActorId;
        }

        public void AddRegionState(IntVector2 coord, RegionState regionState)
        {
            if (!regions.ContainsKey(coord.x))
            {
                regions.Add(coord.x, new Dictionary<int, RegionState>());
            }

            regions[coord.x][coord.y] = regionState;
        }

        public bool TryGetRegionState(IntVector2 coord, out RegionState regionState)
        {
            if (regions.TryGetValue(coord.x, out Dictionary<int, RegionState> yDict))
            {
                if (yDict.TryGetValue(coord.y, out RegionState rs))
                {
                    regionState = rs;
                    return true;
                }
            }

            regionState = null;
            return false;
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}