using System.Threading.Tasks;
using Tofunaut.Core;
using Tofunaut.Core.Interfaces;
using Tofunaut.GridRPG.Data;
using Tofunaut.GridRPG.Game;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Tofunaut.GridRPG
{
    public class GameControllerModel
    {
        public AppContext AppContext;
        public AssetReference GameContextAssetReference;
        public MapData InitialMap;
    }
    
    public class GameController : Controller<GameControllerModel>
    {
        private IRouter _router;
        private GameControllerModel _model;
        private GameView _gameView;
        private GameContext _gameContext;
        
        public override async Task Load(IRouter router, GameControllerModel model)
        {
            _router = router;
            _model = model;

            _gameContext = (await Addressables.InstantiateAsync(_model.GameContextAssetReference).Task).GetComponent<GameContext>();
            
            var gameViewModel = new GameViewModel
            {

            };
            
            _gameView = await model.AppContext.ViewService.Push<GameView, GameViewModel>(gameViewModel);

            var playerActor = (await Addressables.InstantiateAsync(AppConstants.AssetPaths.Prefabs.PlayerActor).Task)
                .GetComponent<Actor>();

            GameContext.PlayerActor = playerActor;
            GameContext.SetPlayerActorInputTarget(playerActor);

            await GameContext.MapManager.SetCurrentMap(model.InitialMap);
            
            GameContext.GameCamera.SetTarget(playerActor.transform);
        }

        public override async Task Unload()
        {
            await _router.Back();
            Destroy(_gameView.gameObject);
        }
    }
}