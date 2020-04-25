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
        private World _world;

        private static class State
        {
            public const string Loading = "loading";
            public const string InGame = "in_game";
        }

        private TofuStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new TofuStateMachine();
            _stateMachine.Register(State.Loading, Loading_Enter, Loading_Update, null);
            _stateMachine.Register(State.InGame, InGame_Enter, null, null);

            _stateMachine.ChangeState(State.Loading);
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }

        private void Loading_Enter()
        {
            World.PreLoadSpriteAtlas(() => { });
        }

        private void Loading_Update(float deltaTime)
        {
            if (AppManager.AssetManager.Ready)
            {
                _stateMachine.ChangeState(State.InGame);
            }
        }

        private void InGame_Enter()
        {
            // check for existing world seed, create one and set it if it doesn't exist
            _worldSeed = PlayerPrefs.GetInt(WorldSeedKey, UnityEngine.Random.Range(int.MinValue, int.MaxValue));
            PlayerPrefs.SetInt(WorldSeedKey, _worldSeed);

            _world = new World(_worldSeed);
        }
    }
}