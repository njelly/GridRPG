using System;

namespace Tofunaut.GridRPG.Game
{
    public static class Collision
    {
        [Flags]
        public enum ELayer
        {
            None = 0,
            Floor = 1 << 0,
            Actor = 1 << 1,
            AirBorn = 1 << 2,

            All = ~None,
        }

        [Serializable]
        public struct Info
        {
            public ELayer solidLayer; // what am I?
            public ELayer collisionLayer; // what do I collide with?

            public bool CollidesWith(Info other)
            {
                return (other.solidLayer & collisionLayer) != 0;
            }
        }
    }
}