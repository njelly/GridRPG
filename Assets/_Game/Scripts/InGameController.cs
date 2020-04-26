using Tofunaut.Core;
using Tofunaut.GridRPG.Game;
using Tofunaut.UnityUtils;
using UnityEngine;

namespace Tofunaut.GridRPG
{
    public class InGameController : ControllerBehaviour
    {
        public const string WorldSeedKey = "world_seed";

        private int _worldSeed;

        private static class State
        {
            public const string Loading = "loading";
            public const string InGame = "in_game";
        }

        private TofuStateMachine _stateMachine;
        private World _world;

        private void Awake()
        {
            _stateMachine = new TofuStateMachine();
            _stateMachine.Register(State.Loading, Loading_Enter, Loading_Update, null);
            _stateMachine.Register(State.InGame, InGame_Enter, null, null);
        }

        private void OnEnable()
        {
            _stateMachine.ChangeState(State.Loading);
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);

            // for debug purposes
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.Complete(new InGameControllerCompletedEventArgs(true, InGameControllerCompletedEventArgs.Code.ReturnToStart));
            }
        }

        private void Loading_Enter()
        {
            World.Load((World payload) =>
            {
                _world = payload;
            });
        }

        private void Loading_Update(float deltaTime)
        {
            if (_world == null)
            {
                return;
            }
            if (!AppManager.AssetManager.Ready)
            {
                return;
            }

            _stateMachine.ChangeState(State.InGame);
        }

        private void InGame_Enter()
        {
            _world.Render(gameObject.transform);
        }

        protected override void Complete(ControllerCompletedEventArgs e)
        {
            _world.Save();
            _world.Destroy();

            base.Complete(e);
        }

        public class InGameControllerCompletedEventArgs : ControllerCompletedEventArgs
        {
            public static class Code
            {
                public const int ReturnToStart = 1;
                public const int ExitApp = 2;
            }

            public readonly int code;

            public InGameControllerCompletedEventArgs(bool succesful, int code) : base(succesful)
            {
                this.code = code;
            }
        }
    }
}