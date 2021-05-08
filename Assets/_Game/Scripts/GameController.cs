using System.Threading.Tasks;
using Tofunaut.Core;
using Tofunaut.Core.Interfaces;

namespace Tofunaut.GridRPG
{
    public class GameControllerModel
    {
    
    }
    
    public class GameController : Controller<GameControllerModel>
    {
        private IRouter _router;
        private GameView _gameView;
        
        public override async Task Load(IRouter router, GameControllerModel model)
        {
            _router = router;
            
            var gameViewModel = new GameViewModel
            {

            };

            _gameView = await router.Context.ViewService.Push<GameView, GameViewModel>(gameViewModel);
        }
    }
}