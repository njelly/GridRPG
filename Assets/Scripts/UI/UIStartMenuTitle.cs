////////////////////////////////////////////////////////////////////////////////
//
//  UIStyleLibrary (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for GridRPG on 10/17/2019
//
////////////////////////////////////////////////////////////////////////////////

using TMPro;
using Tofunaut.Animation;
using Tofunaut.SharpUnity.UI;
using Tofunaut.SharpUnity.UI.Behaviour;
using Tofunaut.UnityUtils;
using UnityEngine;

namespace Tofunaut.GridRPG.UI
{
    // --------------------------------------------------------------------------------------------
    public class UIStartMenuTitle : SharpUIBase
    {
        private SharpUITextMeshPro _label;

        // --------------------------------------------------------------------------------------------
        public UIStartMenuTitle() : base("StartMenuTitle")
        {
            _label = UIStyleLibrary.Text.Default("StartMenuTitle_Label", "GridRPG");
            _label.SetFillSize();
            _label.TextAlignment = TextAlignmentOptions.Center;
            _label.AutoSizeFont();
            AddChild(_label);
        }
    }
}