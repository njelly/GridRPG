using System;
using System.Collections.Generic;

namespace Tofunaut.GridRPG.Game
{
    [Serializable]
    public class TileState
    {
        public enum EType
        {
            Invalid = 0,
            Grass = 1,
        }

        public int type;
        public int variant;
        public int solidLayer;
        public HashSet<ActorState> actors;

        public TileState()
        {
            variant = 0;
            type = 1;
            solidLayer = (int)Collision.ELayer.None;
            actors = new HashSet<ActorState>();
        }
    }
}