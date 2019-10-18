////////////////////////////////////////////////////////////////////////////////
//
//  AppManager (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for GridRPG on 10/14/2019
//
////////////////////////////////////////////////////////////////////////////////

using Tofunaut.Core;
using Tofunaut.UnityUtils;
using Tofunaut.SharpUnity;
using Tofunaut.SharpUnity.UI;
using UnityEngine;

namespace Tofunaut.GridRPG
{
    // --------------------------------------------------------------------------------------------
    public class AppManager : SingletonBehaviour<AppManager>
    {

        // --------------------------------------------------------------------------------------------
        private class State
        {
            public const string Initializing = "initializing";
            public const string AppStartup = "app_startup";
            public const string StartMenu = "start_menu";
            public const string InGame = "in_game";
        }


        // --------------------------------------------------------------------------------------------
        public static Vector2 ReferenceResolution { get { return new Vector2(1920, 1080); } }


        // --------------------------------------------------------------------------------------------
        public static SharpCanvas MainCanvas { get { return _instance._mainCanvas; } }


        // --------------------------------------------------------------------------------------------
        public static SharpLight Sun { get { return _instance._sun; } }


        // --------------------------------------------------------------------------------------------
        public static Version Version { get { return _instance._version; } }


        // --------------------------------------------------------------------------------------------
        public static AssetManager AssetManager { get { return _instance._assetManager; } }

        private TofuStateMachine _stateMachine;
        private Version _version;
        private SharpCamera _mainCamera;
        private SharpCanvas _mainCanvas;
        private SharpLight _sun;
        private int _initializingFrameCounter;
        private AssetManager _assetManager;


        // --------------------------------------------------------------------------------------------
        protected override void Awake()
        {
            base.Awake();

            _version = string.Format("{0}{1}{2}", Application.version, Version.Delimeter, BuildNumberUtil.ReadBuildNumber());

            Debug.LogFormat("{0} v{1} (c) {2} {3}", Application.productName, _version, Application.companyName, "2019");

            DontDestroyOnLoad(gameObject);

            _stateMachine = new TofuStateMachine();
            _stateMachine.Register(State.Initializing, Initializing_Enter, Initializing_Update, Initializing_Exit);
            _stateMachine.Register(State.AppStartup, AppStartup_Enter, null, null);
            _stateMachine.Register(State.StartMenu, StartMenu_Enter, null, null);
            _stateMachine.Register(State.InGame, InGame_Enter, null, null);
            _stateMachine.ChangeState(State.Initializing);
        }


        // --------------------------------------------------------------------------------------------
        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }

        #region State Machine


        // --------------------------------------------------------------------------------------------
        private void Initializing_Enter()
        {
            Debug.Log("Initializing_Enter");

            // load font asset
            _assetManager = new AssetManager();
            _assetManager.Load<TMPro.TMP_FontAsset>(AssetPaths.UI.Fonts.Polsyh);
            _assetManager.Load<TMPro.TMP_FontAsset>(AssetPaths.UI.Fonts.TechnaSans);

            _sun = SharpLight.Sun();
            _sun.Render(transform);

            _mainCamera = SharpCamera.Main();
            _mainCamera.Render(transform);
            _mainCamera.GameObject.SetActive(false);

            _mainCanvas = SharpCanvas.RenderScreenSpaceOverlay("MainCanvas", ReferenceResolution, gameObject.transform);
        }


        // --------------------------------------------------------------------------------------------
        private void Initializing_Update(float deltaTime)
        {
            _initializingFrameCounter++;
            if(_initializingFrameCounter > 2 && _assetManager.Ready)
            {
                _stateMachine.ChangeState(State.AppStartup);
            }
        }


        // --------------------------------------------------------------------------------------------
        private void Initializing_Exit()
        {
            _mainCamera.GameObject.SetActive(true);
        }


        // --------------------------------------------------------------------------------------------
        private void AppStartup_Enter()
        {
            Debug.Log("AppStartup_Enter");

            AppStartupController appStartupController = gameObject.RequireComponent<AppStartupController>();
            appStartupController.enabled = true;
            appStartupController.Completed += AppStartupController_Completed;
        }


        // --------------------------------------------------------------------------------------------
        private void StartMenu_Enter()
        {
            Debug.Log("StartMenu_Enter");

            StartMenuController startMenuController = gameObject.RequireComponent<StartMenuController>();
            startMenuController.enabled = true;
            startMenuController.Completed += StartMenuController_Completed;
        }


        // --------------------------------------------------------------------------------------------
        private void InGame_Enter()
        {
            Debug.Log("InGame_Enter");

            InGameController inGameController = gameObject.RequireComponent<InGameController>();
            inGameController.enabled = true;
            inGameController.Completed += InGameController_Completed;
        }

        #endregion State Machine


        // --------------------------------------------------------------------------------------------
        public void QuitApp()
        {
            Application.Quit();
        }


        // --------------------------------------------------------------------------------------------
        private void AppStartupController_Completed(object sender, ControllerCompletedEventArgs e)
        {
            AppStartupController appStartupController = sender as AppStartupController;
            appStartupController.Completed -= AppStartupController_Completed;
            appStartupController.enabled = false;

            _stateMachine.ChangeState(State.StartMenu);
        }


        // --------------------------------------------------------------------------------------------
        private void StartMenuController_Completed(object sender, ControllerCompletedEventArgs e)
        {
            StartMenuController startMenuController = gameObject.RequireComponent<StartMenuController>();
            startMenuController.enabled = false;
            startMenuController.Completed -= StartMenuController_Completed;

            StartMenuController.StartMenuControllerCompletedEventArgs startMenuArgs = e as StartMenuController.StartMenuControllerCompletedEventArgs;
            switch (startMenuArgs.intention)
            {
                case StartMenuController.StartMenuControllerCompletedEventArgs.Intention.EnterGame:
                    _stateMachine.ChangeState(State.InGame);
                    break;
                case StartMenuController.StartMenuControllerCompletedEventArgs.Intention.QuitApp:
                    QuitApp();
                    break;
            }
        }


        // --------------------------------------------------------------------------------------------
        private void InGameController_Completed(object sender, ControllerCompletedEventArgs e)
        {
            InGameController inGameController = gameObject.RequireComponent<InGameController>();
            inGameController.enabled = false;
            inGameController.Completed -= InGameController_Completed;
        }
    }
}
