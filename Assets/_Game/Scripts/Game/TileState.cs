using System;
using System.Collections.Generic;

namespace Tofunaut.GridRPG.Game
{
    [Serializable]
    public class TileState
    {
        public List<ActorState> actors;

        public TileState()
        {
            actors = new List<ActorState>();
        }
    }
}