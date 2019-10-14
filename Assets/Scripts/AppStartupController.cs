////////////////////////////////////////////////////////////////////////////////
//
//  AppManager (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for GridRPG on 10/14/2019
//
////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using Tofunaut.Core;
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


        // --------------------------------------------------------------------------------------------
        private void Awake()
        {
            _stateMachine = new TofuStateMachine();
            _stateMachine.Register(State.LogIn, LogIn_Enter, null, null);
            _stateMachine.Register(State.LoadAssets, null, null, null);

            _stateMachine.ChangeState(State.LogIn);
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
            List<SessionProvider> sessionProviders = new List<SessionProvider>()
            {
                new UnityDeviceIdSessionProvider(),
            };

            PlayFabAccountAuthenticationManager authenticationManager = new PlayFabAccountAuthenticationManager(sessionProviders, SystemInfo.deviceUniqueIdentifier, AppConsts.PlayFab.TitleID);
            authenticationManager.Authenticate(() =>
            {
                Debug.Log("Authenticated succesfully!");
            }, (TofuError errorCode, string errorMessage) =>
            {
                Debug.LogErrorFormat("Could not authenticate with PlayFab, code {0}: {1}", errorCode, errorMessage);
            });
        }

        #endregion State Machine

    }
}