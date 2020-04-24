using Tofunaut.SharpUnity;

namespace Tofunaut.GridRPG.Game
{
    public class World : SharpGameObject
    {
        public readonly int seed;

        public World(int seed) : base("World")
        {
            this.seed = seed;
        }
    }
}