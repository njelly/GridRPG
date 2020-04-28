using Tofunaut.Animation;
using Tofunaut.Core;
using Tofunaut.SharpUnity;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class Actor : SharpGameObject
    {
        public Tile CurrentTile { get; private set; }
        public bool IsMoving => _moveAnim != null;
        public uint Id => _state.id;

        private readonly ActorState _state;

        private Input _input;
        private TofuAnimation _moveAnim;

        protected Actor(Tile tile, ActorState state) : base("Actor")
        {
            _state = state;

            _input = new Input();

            SetTile(tile, true);
        }

        public void SetTile(Tile tile, bool teleport)
        {
            _moveAnim?.Stop();

            Vector3 targetPos = tile.LocalPosition;

            if (teleport)
            {
                _moveAnim = null;
                LocalPosition = targetPos;
            }
            else
            {
                Vector3 startPos = LocalPosition;
                float animTime = Mathf.Abs(_state.moveSpeed) > 0f ? _state.moveSpeed : float.MaxValue;
                _moveAnim = new TofuAnimation()
                    .Value01(animTime, EEaseType.Linear, (float newValue) =>
                    {
                        LocalPosition = Vector3.LerpUnclamped(startPos, targetPos, newValue);
                    })
                    .Then()
                    .Execute(() =>
                    {
                        _moveAnim = null;
                    })
                    .Play();
            }
        }

        public class Input
        {
            public DirectionButton direction = new DirectionButton();
            public Button actionButton = new Button();

            public class Button
            {
                public float timePressed;
                public float timeReleased;

                public bool Pressed => Time.time - timePressed <= Time.deltaTime;
                public bool Held => timePressed > timeReleased;
                public bool Released => Time.time - timeReleased <= Time.deltaTime;
            }

            public class DirectionButton : Button
            {
                public IntVector2 Direction { get; private set; } = IntVector2.Zero;

                public void SetDirection(IntVector2 direction, float deltaTime)
                {
                    if (Direction != direction)
                    {
                        if (direction.ManhattanDistance > 0)
                        {
                            timePressed = Time.time;
                        }
                        else
                        {
                            timeReleased = Time.time;
                        }
                    }

                    Direction = direction;
                }

                public static implicit operator IntVector2(DirectionButton button)
                {
                    return button.Held ? button.Direction : IntVector2.Zero;
                }
            }
        }

        public static Actor Create(World world, Tile tile)
        {
            ActorState actorState = new ActorState();
            actorState.id = world.GetActorId();

            Actor toReturn = new Actor(tile, actorState);

            return toReturn;
        }
    }
}