namespace Tofunaut.GridRPG.Game
{
    public class Player
    {
        public Actor CurrentActor => _actor;

        private readonly World _world;
        private readonly PlayerState _state;

        private Actor _actor;

        public Player(World world, Actor actor, PlayerState state)
        {
            _world = world;
            _state = state;

            SetActor(actor);
        }

        public void SetActor(Actor actor)
        {
            _state.actorId = actor.Id;
        }
    }
}