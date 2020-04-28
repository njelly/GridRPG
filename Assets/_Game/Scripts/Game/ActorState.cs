using System;
using System.Collections.Generic;
using Tofunaut.Core;

namespace Tofunaut.GridRPG.Game
{
    [Serializable]
    public class ActorState
    {
        public const uint InvalidId = 0;

        public uint id;
        public int variant;
        public float moveSpeed;
        public HashSet<ComponentState> components;
        public IntVector2 currentRegion;
        public IntVector2 currentTile;

        public ActorState()
        {
            id = InvalidId;
            variant = 0;
            moveSpeed = 5f;
            components = new HashSet<ComponentState>();
        }
    }
}