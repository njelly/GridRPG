namespace Tofunaut.GridRPG.Game
{
    public abstract class Component
    {
        public readonly Actor _actor;

        protected Component(Actor actor)
        {
            _actor = actor;
        }
    }
}