using System.Linq;
using System.Threading.Tasks;
using Tofunaut.Core;
using Tofunaut.Core.Interfaces;
using Tofunaut.GridRPG.Data;
using Tofunaut.GridRPG.Game;
using UnityEngine;

namespace Tofunaut.GridRPG
{
    public class StartScreenModel
    {
    }
    
    public class StartScreenController : Controller<StartScreenModel>
    {
        private IRouter _router;
        private StartScreenView _startScreenView;
        private StartScreenModel _model;
        
        public override async Task Load(IRouter router, StartScreenModel model)
        {
            _model = model;
            _router = router;
            
            var startScreenViewModel = new StartScreenViewModel
            {
                OnPlayPressed = OnPlayPressed,
            };

            _startScreenView = await AppContext.ViewStack.Push<StartScreenView, StartScreenViewModel>(AppConstants.AssetPaths.UI.StarScreenView, startScreenViewModel);
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
                InitialMap = AppContext.MapCatalog.MapDatas.First(),
            });
        }
    }
}