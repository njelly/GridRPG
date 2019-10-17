//////////////////////////////////////////////////////////////////////////////
//
//  AppStartupView (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for TofuUnity on 10/15/2019
//
////////////////////////////////////////////////////////////////////////////////

using TMPro;
using Tofunaut.SharpUnity.UI;
using UnityEngine;

namespace Tofunaut.GridRPG.UI
{
    public class AppStartupView : SharpUIView
    {
        private SharpUITextMeshPro _messageLabel;

        public AppStartupView() : base(AppManager.MainCanvas) { }

        protected override SharpUIBase BuildMainPanel()
        {
            SharpUIImage toReturn = new SharpUIImage("AppStartupView", null);
            toReturn.SetFillSize();
            toReturn.Color = Color.gray;

            _messageLabel = UIStyleLibrary.Text.Default("MessageLabel", string.Empty, Color.white);
            _messageLabel.alignment = EAlignment.BottomCenter;
            _messageLabel.SetFillSize(EAxis.X);
            _messageLabel.SetFixedSize(EAxis.Y, 100);
            _messageLabel.margin = new RectOffset(0, 0, 0, 50);
            _messageLabel.AutoSize = true;
            _messageLabel.TextAlignment = TextAlignmentOptions.Center;
            toReturn.AddChild(_messageLabel);

            return toReturn;
        }

        public void SetMessage(string message)
        {
            _messageLabel.Text = message;
        }
    }
}