using System.Threading.Tasks;
using Tofunaut.Core;
using Tofunaut.Core.Interfaces;
using Tofunaut.GridRPG.Game;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Tofunaut.GridRPG
{
    public class GameControllerModel
    {
        
    }
    
    public class GameController : Controller<GameControllerModel>
    {
        private IRouter _router;
        private GameControllerModel _model;
        private GameView _gameView;
        private SceneInstance _sceneInstance;
        private Actor _playerActor;
        
        public override async Task Load(IRouter router, GameControllerModel model)
        {
            _router = router;
            _model = model;
            
            var gameViewModel = new GameViewModel
            {

            };

            _sceneInstance = await Addressables.LoadSceneAsync(AppConstants.AssetPaths.Scenes.Game, LoadSceneMode.Additive).Task;
            SceneManager.SetActiveScene(_sceneInstance.Scene);
            
            _gameView = await router.Context.ViewService.Push<GameView, GameViewModel>(gameViewModel);

            _playerActor = (await Addressables.InstantiateAsync(AppConstants.AssetPaths.Prefabs.PlayerActor).Task)
                .GetComponent<Actor>();
        }

        public override async Task Unload()
        {
            SceneManager.UnloadSceneAsync(_sceneInstance.Scene);
            await _router.Back();
            Destroy(_gameView.gameObject);
        }
    }
}