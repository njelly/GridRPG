using System;
using System.Collections.Generic;
using Tofunaut.Core;
using Tofunaut.SharpUnity;

namespace Tofunaut.GridRPG.Game
{
    public class Tile : SharpGameObject
    {
        public readonly IntVector2 coord;
        public readonly Region region;
        public readonly Config config;
        public readonly TileView view;

        private List<Actor> _occupants;

        public Tile(IntVector2 coord, Region region, Config config) : base($"{region.Name}_{coord.ToString()}")
        {
            this.coord = coord;
            this.region = region;
            this.config = config;

            LocalPosition = new UnityEngine.Vector3(coord.x, coord.y, 0);

            view = new TileView(this);

            _occupants = new List<Actor>();
        }

        [Serializable]
        public struct Config
        {
            public Collision.Info defaultCollision;
            public int groundSpriteIndex;
            public int mainSpriteIndex;

            public static Config Field_Open => new Config
            {
                defaultCollision = new Collision.Info
                {
                    solidLayer = Collision.ELayer.None,
                    collisionLayer = Collision.ELayer.None,
                },
                groundSpriteIndex = 0,
                mainSpriteIndex = -1,
            };

            public static Config Tree_Default => new Config
            {
                defaultCollision = new Collision.Info
                {
                    solidLayer = Collision.ELayer.All,
                    collisionLayer = Collision.ELayer.All,
                },
                groundSpriteIndex = 5,
                mainSpriteIndex = -1,
            };

            public static Config Grass_Default => new Config
            {
                defaultCollision = new Collision.Info
                {
                    solidLayer = Collision.ELayer.Floor,
                    collisionLayer = Collision.ELayer.Floor,
                },
                groundSpriteIndex = 2,
                mainSpriteIndex = -1,
            };
        }
    }
}