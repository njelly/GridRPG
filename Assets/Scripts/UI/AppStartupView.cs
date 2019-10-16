//////////////////////////////////////////////////////////////////////////////
//
//  AppStartupView (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for TofuUnity on 10/15/2019
//
////////////////////////////////////////////////////////////////////////////////

using Tofunaut.SharpUnity.UI;
using UnityEngine;

namespace Tofunaut.GridRPG.UI
{
    public class AppStartupView : SharpUIView
    {

        public AppStartupView() : base(AppManager.MainCanvas) { }

        protected override SharpUIBase BuildMainPanel()
        {
            SharpUIImage toReturn = new SharpUIImage("AppStartupView", null);
            toReturn.SetFillSize();
            toReturn.Color = Color.gray;

            return toReturn;
        }
    }
}