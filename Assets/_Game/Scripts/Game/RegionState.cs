using System;
using System.Collections.Generic;
using Tofunaut.Core;

namespace Tofunaut.GridRPG.Game
{
    [Serializable]
    public class RegionState
    {
        public const int Size = 16;

        public TileState[,] tiles;
        public List<ActorState> actors;
        public IntVector2 coord;

        public RegionState()
        {
            tiles = new TileState[Size, Size];
            actors = new List<ActorState>();
            coord = IntVector2.Zero;
        }
    }
}