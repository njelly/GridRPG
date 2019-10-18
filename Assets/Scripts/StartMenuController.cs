////////////////////////////////////////////////////////////////////////////////
//
//  AppManager (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for GridRPG on 10/14/2019
//
////////////////////////////////////////////////////////////////////////////////

using Tofunaut.GridRPG.UI;
using Tofunaut.UnityUtils;
using UnityEngine;

namespace Tofunaut.GridRPG
{

    // --------------------------------------------------------------------------------------------
    public class StartMenuController : ControllerBehaviour, StartMenuView.IStartMenuViewListener
    {
        private StartMenuView _startMenuView;


        // --------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            _startMenuView = new StartMenuView(this);
            _startMenuView.Show();
        }


        // --------------------------------------------------------------------------------------------
        protected override void OnDisable()
        {
            base.OnDisable();

            _startMenuView.Hide();
        }


        // --------------------------------------------------------------------------------------------
        public void OnPlayClicked()
        {
            Complete(new StartMenuControllerCompletedEventArgs(true, StartMenuControllerCompletedEventArgs.Intention.EnterGame));
        }

        public class StartMenuControllerCompletedEventArgs : ControllerCompletedEventArgs
        {
            public enum Intention
            {
                EnterGame,
                QuitApp,
            }

            public readonly Intention intention;

            public StartMenuControllerCompletedEventArgs(bool succesful, Intention intention) : base(succesful)
            {
                this.intention = intention;
            }
        }
    }
}
