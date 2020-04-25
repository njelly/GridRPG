using System;
using System.Threading;
using Tofunaut.Animation;
using Tofunaut.Core;

namespace Tofunaut.GridRPG.Game
{
    public static class RegionGeneration
    {
        public enum EAlgorithm
        {
            Invalid = 0,
            Default = 1,
        }

        [Serializable]
        public struct Config
        {
            public int seed;
            public int width;
            public int height;
            public EAlgorithm algorithm;
        }

        public static void Generate(World world, IntVector2 coord, Config config, Action<Region> onComplete)
        {
            object lockObj = new object();
            Region region = null;
            Thread genThread = new Thread(new ThreadStart(() =>
            {
                Region temp;
                switch (config.algorithm)
                {
                    case EAlgorithm.Default:
                        temp = DefaultGeneration(world, coord, config);
                        break;
                    default:
                        throw new Exception($"the alorithm type {config.algorithm} is not implemented");
                }

                lock (lockObj)
                {
                    region = temp;
                }
            }));


            TofuAnimation waitForGeneration = new TofuAnimation()
                .Execute(() =>
                {
                    genThread.Start();
                })
                .WaitUntil(() =>
                {
                    lock (lockObj)
                    {
                        return region != null;
                    }
                })
                .Then()
                .Execute(() =>
                {
                    onComplete(region);
                })
                .Play();

        }

        private static Region DefaultGeneration(World world, IntVector2 coord, Config config)
        {
            Tile.Config[,] _tiles = new Tile.Config[config.width, config.height];
            for (int x = 0; x < config.width; x++)
            {
                for (int y = 0; y < config.height; y++)
                {
                    if (x == 0 || y == 0 || x == config.width || y == config.height)
                    {
                        _tiles[x, y] = Tile.Config.Tree_Default;
                    }
                    else
                    {
                        _tiles[x, y] = Tile.Config.Field_Open;
                    }
                }
            }

            return new Region(world, coord, _tiles);
        }
    }
}