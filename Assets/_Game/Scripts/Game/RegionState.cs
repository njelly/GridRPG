using System;

namespace Tofunaut.GridRPG.Game
{
    [Serializable]
    public class RegionState
    {
        public const int Size = 16;

        public TileState[,] tiles;

        public RegionState()
        {
            tiles = new TileState[Size, Size];
        }
    }
}