using System.Threading.Tasks;
using Tofunaut.Core;
using Tofunaut.Core.Interfaces;
using Tofunaut.GridRPG.Game;
using UnityEngine;

namespace Tofunaut.GridRPG
{
    public class StartScreenModel
    {
        
    }
    
    [CreateAssetMenu(fileName = "new StartScreenController", menuName = "Tofunaut/GridRPG/StartScreenController")]
    public class StartScreenController : Controller<StartScreenModel>
    {
        private IRouter _router;
        private StartScreenView _startScreenView;
        
        public override async Task Load(IRouter router, StartScreenModel model)
        {
            _router = router;
            
            var startScreenViewModel = new StartScreenViewModel
            {
                OnPlayPressed = OnPlayPressed,
            };

            _startScreenView = await router.Context.ViewService.Push<StartScreenView, StartScreenViewModel>(startScreenViewModel);
        }

        public override Task Unload()
        {
            Destroy(_startScreenView.gameObject);
            return Task.CompletedTask;
        }

        private async void OnPlayPressed()
        {
            await _router.ReplaceCurrent<GameController, GameControllerModel>(new GameControllerModel
            {
                
            });
        }
    }
}