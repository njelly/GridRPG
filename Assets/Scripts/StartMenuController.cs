////////////////////////////////////////////////////////////////////////////////
//
//  AppManager (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for GridRPG on 10/14/2019
//
////////////////////////////////////////////////////////////////////////////////

using Tofunaut.GridRPG.UI;
using Tofunaut.UnityUtils;

namespace Tofunaut.GridRPG
{
    public class StartMenuController : ControllerBehaviour, StartMenuView.IStartMenuViewListener
    {
        StartMenuView _startMenuView;

        private void OnEnable()
        {
            _startMenuView = new StartMenuView(this);
            _startMenuView.Show();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _startMenuView.Hide();
        }
    }
}
