using Tofunaut.Core;
using Tofunaut.UnityUtils;
using UnityEngine;

namespace Tofunaut.GridRPG
{
    public class AppManager : SingletonBehaviour<AppManager>
    {
        private static class State 
        {
            public const string Initialize = "initialize";
            public const string StartMenu = "start_menu";
            public const string InGame = "in_game";
        }

        public static AssetManager AssetManager => _instance._assetManager;

        private TofuStateMachine _stateMachine;
        private AssetManager _assetManager;

#if UNITY_EDITOR
        [Header("Development")]
        [SerializeField] private bool _skipStartMenu = false;
#endif

        protected override void Awake()
        {
            base.Awake();

            _assetManager = new AssetManager();

            _stateMachine = new TofuStateMachine();
            _stateMachine.Register(State.Initialize, Initialize_Enter, Initialize_Update, null);
            _stateMachine.Register(State.StartMenu, StartMenu_Enter, null, null);
            _stateMachine.Register(State.InGame, InGame_Enter, null, null);

            _stateMachine.ChangeState(State.Initialize);
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }

        private void Initialize_Enter()
        {
            _assetManager.Load<TMPro.TMP_FontAsset>(AssetPaths.Fonts.LiberationBold);
            _assetManager.Load<TMPro.TMP_FontAsset>(AssetPaths.Fonts.LiberationBoldItalic);
            _assetManager.Load<TMPro.TMP_FontAsset>(AssetPaths.Fonts.LiberationItalic);
            _assetManager.Load<TMPro.TMP_FontAsset>(AssetPaths.Fonts.LiberationRegular);
        }

        private void Initialize_Update(float deltaTime)
        {
            if(_assetManager.Ready)
            {
#if UNITY_EDITOR
                if(_skipStartMenu)
                {
                    _stateMachine.ChangeState(State.InGame);
                }
                else
                {
                    _stateMachine.ChangeState(State.StartMenu);
                }
#else
                _stateMachine.ChangeState(State.StartMenu);
#endif
            }
        }

        private void StartMenu_Enter()
        {
            Debug.Log("StartMenu_Enter");
        }

        private void InGame_Enter()
        {
            Debug.Log("InGame_Enter");
        }
    }
}