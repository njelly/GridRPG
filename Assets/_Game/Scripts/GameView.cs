using System;
using System.Threading.Tasks;
using Tofunaut.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tofunaut.GridRPG
{
    public class GameViewModel
    {
        public Action OnBack;
    }
    
    public class GameView : ViewController<GameViewModel>
    {
        public override Task Initialize(GameViewModel model)
        {
            return Task.CompletedTask;
        }
    }
}