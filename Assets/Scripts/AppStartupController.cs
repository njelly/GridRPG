////////////////////////////////////////////////////////////////////////////////
//
//  AppManager (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for GridRPG on 10/14/2019
//
////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using Tofunaut.Core;
using Tofunaut.GridRPG.UI;
using Tofunaut.PlayFabUtils;
using Tofunaut.UnityUtils;
using UnityEngine;

// --------------------------------------------------------------------------------------------
namespace Tofunaut.GridRPG
{
    // --------------------------------------------------------------------------------------------
    public class AppStartupController : ControllerBehaviour
    {

        // --------------------------------------------------------------------------------------------
        private class State
        {
            public const string LogIn = "log_in";
            public const string LoadAssets = "load_assets";
        }

        private TofuStateMachine _stateMachine;
        private PlayFabAccountAuthenticationManager _authenticationManager;
        private AppStartupView _appStartupView;


        // --------------------------------------------------------------------------------------------
        private void Awake()
        {
            _stateMachine = new TofuStateMachine();
            _stateMachine.Register(State.LogIn, LogIn_Enter, null, null);
            _stateMachine.Register(State.LoadAssets, LoadAssets_Enter, null, null);

            _stateMachine.ChangeState(State.LogIn);
        }


        // --------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            _appStartupView = new AppStartupView();
            _appStartupView.Show();
        }


        // --------------------------------------------------------------------------------------------
        protected override void OnDisable()
        {
            _appStartupView.Hide();
        }


        // --------------------------------------------------------------------------------------------
        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }

        #region State Machine


        // --------------------------------------------------------------------------------------------
        private void LogIn_Enter()
        {
            _appStartupView.SetMessage("Authenticating...");

            List<SessionProvider> sessionProviders = new List<SessionProvider>()
            {
                new UnityDeviceIdSessionProvider(),
            };

            _authenticationManager = new PlayFabAccountAuthenticationManager(sessionProviders, SystemInfo.deviceUniqueIdentifier, AppConsts.PlayFab.TitleID);
            _authenticationManager.Authenticate(() =>
            {
                Debug.Log("Authenticated succesfully!");
                _stateMachine.ChangeState(State.LoadAssets);
            }, (TofuError errorCode, string errorMessage) =>
            {
                Debug.LogErrorFormat("Could not authenticate with PlayFab, code {0}: {1}", errorCode, errorMessage);
            });
        }


        // --------------------------------------------------------------------------------------------
        private void LoadAssets_Enter()
        {
            // TODO: actually load assets
            Complete(new AppStartupControllerCompletedEventArgs(true, _authenticationManager.AccountData));
        }

        #endregion State Machine

        public class AppStartupControllerCompletedEventArgs : ControllerCompletedEventArgs
        {
            public readonly PlayFabAccountData accountData;

            public AppStartupControllerCompletedEventArgs(bool successful, PlayFabAccountData accountData) : base(successful)
            {
                this.accountData = accountData;
            }
        }

    }
}
