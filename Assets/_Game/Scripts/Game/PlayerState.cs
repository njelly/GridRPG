using Tofunaut.Core;

namespace Tofunaut.GridRPG.Game
{
    public class PlayerState
    {
        public uint actorId;

        public PlayerState()
        {
            actorId = ActorState.InvalidId;
        }
    }
}