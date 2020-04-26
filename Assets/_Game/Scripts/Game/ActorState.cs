using System;
using System.Collections.Generic;

namespace Tofunaut.GridRPG.Game
{
    [Serializable]
    public class ActorState
    {
        public int variant;
        public float moveSpeed;
        public HashSet<ComponentState> components;

        public ActorState()
        {
            variant = 0;
            moveSpeed = 5f;
            components = new HashSet<ComponentState>();
        }
    }
}