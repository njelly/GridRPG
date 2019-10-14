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

        private TofuStateMachine _stateMachine;
        private Version _version;
        private SharpCamera _mainCamera;
        private SharpCanvas _mainCanvas;
        private SharpLight _sun;
        private int _initializingFrameCounter;


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
            _stateMachine.Register(State.InGame, null, null, null);
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
            if(_initializingFrameCounter > 2)
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
            AppStartupController appStartupController = gameObject.RequireComponent<AppStartupController>();
            appStartupController.enabled = true;
            appStartupController.Completed += AppStartupController_Completed;
        }


        // --------------------------------------------------------------------------------------------
        private void StartMenu_Enter()
        {
            Debug.Log("StartMenu_Enter");
        }

        #endregion State Machine


        // --------------------------------------------------------------------------------------------
        private void AppStartupController_Completed(object sender, ControllerCompletedEventArgs e)
        {
            AppStartupController appStartupController = sender as AppStartupController;
            appStartupController.Completed -= AppStartupController_Completed;
            appStartupController.enabled = false;

            _stateMachine.ChangeState(State.StartMenu);
        }
    }
}
