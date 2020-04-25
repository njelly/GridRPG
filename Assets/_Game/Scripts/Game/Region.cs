using System;
using Tofunaut.Core;
using Tofunaut.SharpUnity;

namespace Tofunaut.GridRPG.Game
{
    public class Region : SharpGameObject
    {
        public enum EType
        {
            Invalid = 0,
            Field = 1,
        }

        [Serializable]
        public struct Config
        {
            public IntVector2 size;
        }

        [Serializable]
        public struct PremadeRoomConfig
        {
            public Tile.Config[,] tileConfigs;
            public IntMargin margin;

            public IntVector2 Size => new IntVector2(tileConfigs.GetLength(0), tileConfigs.GetLength(1));
        }

        public readonly World world;
        public readonly IntVector2 coord;



        protected readonly Tile[,] _tiles;

        public Region(World world, Tile.Config[,] tileConfigs) : base($"Region {coord.ToString()}")
        {
            this.world = world;
            this.coord = coord;

            _tiles = new Tile[tileConfigs.GetLength(0), tileConfigs.GetLength(1)];
            for (int x = 0; x < tileConfigs.GetLength(0); x++)
            {
                for (int y = 0; y < tileConfigs.GetLength(1); y++)
                {
                    _tiles[x, y] = new Tile(new IntVector2(x, y), this, tileConfigs[x, y]);
                }
            }
        }


    }
}