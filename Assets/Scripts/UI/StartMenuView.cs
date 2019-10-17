////////////////////////////////////////////////////////////////////////////////
//
//  StartMenuView (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for TofuUnity on 10/15/2019
//
////////////////////////////////////////////////////////////////////////////////

using Tofunaut.SharpUnity.UI;
using UnityEngine;

namespace Tofunaut.GridRPG.UI
{
    public class StartMenuView : SharpUIView
    {
        public interface IStartMenuViewListener
        {

        }

        private readonly IStartMenuViewListener _listener;

        public StartMenuView(IStartMenuViewListener listener) : base(AppManager.MainCanvas)
        {
            _listener = listener;
        }

        protected override SharpUIBase BuildMainPanel()
        {
            SharpUIImage toReturn = new SharpUIImage("StartMenuView", null);
            toReturn.SetFillSize();
            toReturn.Color = Color.gray;

            return toReturn;
        }
    }
}