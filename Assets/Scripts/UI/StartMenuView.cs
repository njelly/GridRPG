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
            void OnPlayClicked();
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

            UIStartMenuTitle titleLabel = new UIStartMenuTitle();
            titleLabel.alignment = EAlignment.TopCenter;
            titleLabel.SetFillSize(EAxis.X, 1f);
            titleLabel.SetFixedSize(EAxis.Y, 200);
            titleLabel.margin = new RectOffset(0, 0, 50, 100);
            toReturn.AddChild(titleLabel);

            UIStartMenuButton playButton = new UIStartMenuButton("PlayButton", "Play", () => { _listener.OnPlayClicked(); });
            playButton.SetFixedSize(250, 90);
            playButton.alignment = EAlignment.MiddleCenter;
            toReturn.AddChild(playButton);

            SharpUITextMeshPro versionLabel = UIStyleLibrary.Text.Default("VersionLabel", AppManager.Version);
            versionLabel.alignment = EAlignment.BottomRight;
            versionLabel.SetFixedSize(100, 50);
            versionLabel.margin = new RectOffset(0, 20, 0, 20);
            versionLabel.TextAlignment = TextAlignmentOptions.BottomRight;
            versionLabel.AutoSizeFont();
            toReturn.AddChild(versionLabel);

            return toReturn;
        }


        // --------------------------------------------------------------------------------------------
        private void OnClickTestButton()
        {
            Debug.Log("Test!");
        }
    }
}