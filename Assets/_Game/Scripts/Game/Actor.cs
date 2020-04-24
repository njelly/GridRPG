using Tofunaut.Core;
using Tofunaut.SharpUnity;

namespace Tofunaut.GridRPG.Game
{
    public abstract class Actor : SharpGameObject
    {
        public Tile CurrentTile => _currentTile;

        protected ActorInput _input;
        protected IntVector2 _interactOffset;

        private Tile _currentTile;

        protected Actor(string name, Tile tile) : base(name)
        {
            _currentTile = tile;
            _input = new ActorInput();
            _interactOffset = IntVector2.Right;
        }

        public void ReceiveInput(ActorInput input)
        {
            _input = input;

            // do stuff with new input
        }
    }

    public struct ActorInput
    {
        public IntVector2 direction;
        public Button actionButton;

        public class Button
        {
            public bool wasDown;
            public float timeDown;

            public bool Released => timeDown <= float.Epsilon && wasDown;
            public bool Held => timeDown > float.Epsilon && wasDown;
            public bool Pressed => timeDown > float.Epsilon && !wasDown;

            public static implicit operator bool(Button button)
            {
                return button.timeDown > float.Epsilon;
            }
        }
    }
}