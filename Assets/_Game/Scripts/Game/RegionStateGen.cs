using System;
using System.Threading;
using Tofunaut.Animation;
using Tofunaut.Core;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public static class RegionStateGen
    {

        public static void GenerateOnThread(IntVector2 coord, Action<RegionState> onComplete)
        {
            object lockObj = new object();
            RegionState regionState = null;
            Thread genThread = new Thread(new ThreadStart(() =>
            {
                RegionState rs = Generate(coord);

                lock (lockObj)
                {
                    regionState = rs;
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
                        return regionState != null;
                    }
                })
                .Then()
                .Execute(() =>
                {
                    onComplete(regionState);
                })
                .Play();
        }

        public static RegionState Generate(IntVector2 coord)
        {
            RegionState toReturn = new RegionState();
            toReturn.coord = coord;

            for (int x = 0; x < toReturn.tiles.GetLength(0); x++)
            {
                for (int y = 0; y < toReturn.tiles.GetLength(1); y++)
                {
                    TileState tileState = new TileState();
                    // TODO: perlin noise to use different variations of the tile

                    toReturn.tiles[x, y] = tileState;
                }
            }

            return toReturn;
        }

    }
}