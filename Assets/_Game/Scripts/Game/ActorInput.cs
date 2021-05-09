using UnityEngine;
using UnityEngine.Animations;

namespace Tofunaut.GridRPG.Game
{
    public class ActorInput
    {
        public readonly DoubleAxis Direction = new DoubleAxis();
        public readonly Button Interacting = new Button();
        
        public abstract class Input
        {
            public float SignalDuration => this ? Time.time - _signalStart : 0f;
            
            protected float _signalStart;
            protected float _signalEnd;

            protected Input()
            {
                _signalStart = float.MaxValue;
                _signalEnd = float.MaxValue;
            }

            public static implicit operator bool(Input input)
            {
                var now = Time.time;
                return now >= input._signalStart && now < input._signalEnd;
            }
        }

        public class Button : Input
        {
            public bool IsPressed
            {
                get => this;
                set
                {
                    switch (value)
                    {
                        case true when !this:
                            _signalStart = Time.time;
                            _signalEnd = float.MaxValue;
                            break;
                        case false when this:
                            _signalEnd = Time.time;
                            break;
                    }
                }
            }
        }

        public class SingleAxis : Input
        {
            public float Axis
            {
                get => _axis;
                set
                {
                    _axis = Mathf.Abs(value) > _deadZone ? value : 0f;
                    if (Mathf.Abs(_axis) >= float.Epsilon && !this)
                    {
                        _signalStart = Time.time;
                        _signalEnd = float.MaxValue;
                    }
                    else if (Mathf.Abs(_axis) < float.Epsilon && this)
                    {
                        _signalEnd = Time.time;
                    }
                }
            }

            private float _axis;
            private readonly float _deadZone;

            public SingleAxis(float deadZone = 0f)
            {
                _deadZone = deadZone;
            }

            public static implicit operator float(SingleAxis singleAxis) => singleAxis._axis;
        }

        public class DoubleAxis : Input
        {
            public Vector2 Axis
            {
                get => _axis;
                set
                {
                    _axis = new Vector2(Mathf.Abs(value.x) > _deadZone.x ? value.x : 0f,
                        Mathf.Abs(value.y) > _deadZone.y ? value.y : 0f);

                    if (_axis.sqrMagnitude >= float.Epsilon && !this)
                    {
                        _signalStart = Time.time;
                        _signalEnd = float.MaxValue;
                    }
                    else if (_axis.sqrMagnitude < float.Epsilon && this)
                    {
                        _signalEnd = Time.time;
                    }
                }
            }

            private Vector2 _axis;
            private readonly Vector2 _deadZone;

            public DoubleAxis()
            {
                _deadZone = Vector2.zero;
            }
            public DoubleAxis(Vector2 deadZone)
            {
                _deadZone = deadZone;
            }

            public static implicit operator Vector2(DoubleAxis doubleAxis) => doubleAxis._axis;
        }
    }
}