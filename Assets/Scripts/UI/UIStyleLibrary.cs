//////////////////////////////////////////////////////////////////////////////
//
//  UIStyleLibrary (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for GridRPG on 10/16/2019
//
////////////////////////////////////////////////////////////////////////////////

using TMPro;
using Tofunaut.SharpUnity.UI;
using UnityEngine;

namespace Tofunaut.GridRPG
{
    public static class UIStyleLibrary
    {
        public static class Text
        {
            public static SharpUITextMeshPro Default(string name, string text, Color color)
            {
                SharpUITextMeshPro toReturn = new SharpUITextMeshPro(name, text);
                toReturn.Font = AppManager.AssetManager.Get<TMP_FontAsset>(AssetPaths.UI.Fonts.Polsyh);

                return toReturn;
            }
        }
    }
}