namespace Tofunaut.GridRPG.Game
{
    public class NoOpActorInputProvider : IActorInputProvider
    {
        public ActorInput Input { get; }

        public NoOpActorInputProvider()
        {
            Input = new ActorInput();
        }
    }
}