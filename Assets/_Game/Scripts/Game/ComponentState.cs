using System.Collections.Generic;

namespace Tofunaut.GridRPG.Game
{
    public abstract class ComponentState
    {
        public enum EType
        {
            Invalid,
            Destructible,
            Inventory,
        }

        public int type;

        protected ComponentState(EType type)
        {
            this.type = (int)type;
        }
    }

    public class DestructibleState : ComponentState
    {
        public const int MaxHealth = 100;

        public int health;

        public DestructibleState() : base(EType.Destructible)
        {
            health = MaxHealth;
        }
    }

    public class InventoryState : ComponentState
    {
        public List<Actor> held;

        public InventoryState() : base(EType.Inventory)
        {
            held = new List<Actor>();
        }
    }
}