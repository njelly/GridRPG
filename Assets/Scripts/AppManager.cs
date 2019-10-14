////////////////////////////////////////////////////////////////////////////////
//
//  AppManager (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for GridRPG on 10/14/2019
//
////////////////////////////////////////////////////////////////////////////////

using Tofunaut.Core;
using Tofunaut.UnityUtils;
using UnityEngine;

namespace Tofunaut.GridRPG
{
    public class AppManager : SingletonBehaviour<AppManager>
    {
        private class State
        {
            public const string Initializing = "initializing";
            public const string AppStartup = "app_startup";
            public const string StartMenu = "start_menu";
            public const string InGame = "in_game";
        }

        private TofuStateMachine _stateMachine;
        private Version _version;

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);

            _version = string.Format("{0}{1}{2}", Application.version, Version.Delimeter, BuildNumberUtil.ReadBuildNumber());

            _stateMachine = new TofuStateMachine();
            _stateMachine.Register(State.Initializing, null, null, null);
            _stateMachine.Register(State.AppStartup, null, null, null);
            _stateMachine.Register(State.StartMenu, null, null, null);
            _stateMachine.Register(State.InGame, null, null, null);
            _stateMachine.ChangeState(State.Initializing);

            Debug.LogFormat("{0} v{1} (c) {2} {3}", Application.productName, _version, Application.companyName, "2019");
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }
    }
}
