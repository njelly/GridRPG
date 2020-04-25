using System;
using System.Collections.Generic;
using Tofunaut.Core;
using Tofunaut.SharpUnity;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class World : SharpGameObject
    {
        public static Sprite[] Sprites { get; private set; }

        public readonly int seed;

        private Region _originRegion;

        public World(int seed) : base("World")
        {
            this.seed = seed;
        }

        public void PreGenerateOrigin(Action onComplete)
        {

        }

        public static void PreLoadSpriteAtlas(Action onComplete)
        {
            AppManager.AssetManager.LoadList(AssetPaths.Textures.SpriteAtlas, (bool succesful, List<Sprite> payload) =>
            {
                if (succesful)
                {
                    Sprites = payload.ToArray();
                }
            });
        }
    }
}