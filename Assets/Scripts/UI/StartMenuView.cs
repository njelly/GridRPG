////////////////////////////////////////////////////////////////////////////////
//
//  StartMenuView (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for TofuUnity on 10/15/2019
//
////////////////////////////////////////////////////////////////////////////////

using TMPro;
using Tofunaut.SharpUnity.UI;
using Tofunaut.SharpUnity.UI.Behaviour;
using UnityEngine;

namespace Tofunaut.GridRPG.UI
{

    // --------------------------------------------------------------------------------------------
    public class StartMenuView : SharpUIView
    {

        // --------------------------------------------------------------------------------------------
        public interface IStartMenuViewListener
        {

        }

        private readonly IStartMenuViewListener _listener;


        // --------------------------------------------------------------------------------------------
        public StartMenuView(IStartMenuViewListener listener) : base(AppManager.MainCanvas)
        {
            _listener = listener;
        }


        // --------------------------------------------------------------------------------------------
        protected override SharpUIBase BuildMainPanel()
        {
            SharpUIImage toReturn = new SharpUIImage("StartMenuView", null);
            toReturn.SetFillSize();
            toReturn.Color = Color.gray;

            SharpUITextMeshPro titleLabel = UIStyleLibrary.Text.Default("TitleLabel", "GridRPG");
            titleLabel.alignment = EAlignment.TopCenter;
            titleLabel.SetFillSize(EAxis.X);
            titleLabel.SetFixedSize(EAxis.Y, 200);
            titleLabel.margin = new RectOffset(0, 0, 50, 100);
            titleLabel.TextAlignment = TextAlignmentOptions.Center;
            titleLabel.AutoSizeFont();
            toReturn.AddChild(titleLabel);

            toReturn.AddChild(BuildTestButton());

            return toReturn;
        }


        // --------------------------------------------------------------------------------------------
        private SharpUIBase BuildTestButton()
        {
            SharpUIImage toReturn = new SharpUIImage("TestButton", null);
            toReturn.SetFixedSize(200, 80);
            toReturn.alignment = EAlignment.BottomCenter;
            toReturn.margin = new RectOffset(0, 0, 0, (int)(AppManager.ReferenceResolution.y / 2));

            SharpUITextMeshPro label = UIStyleLibrary.Text.Default("TestButton_Label", "Test");
            label.TextAlignment = TextAlignmentOptions.Center;
            label.SetFillSize();
            label.Color = Color.black;
            toReturn.AddChild(label);

            toReturn.SubscribeToEvent(EEventType.PointerClick, OnClickTestButton);

            return toReturn;
        }


        // --------------------------------------------------------------------------------------------
        private void OnClickTestButton(object sender, EventSystemEventArgs e)
        {
            Debug.Log("Test!");
        }
    }
}