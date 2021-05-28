using System;
using System.Threading.Tasks;
using Tofunaut.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Tofunaut.GridRPG
{
    public class StartScreenViewModel
    {
        public Action OnPlayPressed;
    }
    
    public class StartScreenView : ViewController<StartScreenViewModel>
    {
        [SerializeField] private Button _playButton;

        public override Task OnPushedToStack(StartScreenViewModel model)
        {
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(() => model.OnPlayPressed?.Invoke());
            
            return Task.CompletedTask;
        }
    }
}