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
        public MapData InitialMap;
    }
    
    public class GameController : Controller<GameControllerModel>
    {
        public Actor PlayerActor { get; private set; }
        public MapManager MapManager { get; private set; }
        
        private IRouter _router;
        private GameControllerModel _model;
        private GameView _gameView;
        private SceneInstance _sceneInstance;
        
        public override async Task Load(IRouter router, GameControllerModel model)
        {
            _router = router;
            _model = model;
            MapManager = new MapManager();
            
            var gameViewModel = new GameViewModel
            {

            };
            
            _gameView = await model.AppContext.ViewService.Push<GameView, GameViewModel>(gameViewModel);

            PlayerActor = (await Addressables.InstantiateAsync(AppConstants.AssetPaths.Prefabs.PlayerActor).Task)
                .GetComponent<Actor>();

            await MapManager.SetCurrentMap(model.InitialMap);
        }

        public override async Task Unload()
        {
            SceneManager.UnloadSceneAsync(_sceneInstance.Scene);
            await _router.Back();
            Destroy(_gameView.gameObject);
        }
    }
}