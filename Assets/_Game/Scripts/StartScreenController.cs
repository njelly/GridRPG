using System.Threading.Tasks;
using Tofunaut.Core;
using Tofunaut.Core.Interfaces;
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

        private async void OnPlayPressed()
        {
            // await _router.GoTo<GameController, GameControllerModel>(new GameControllerModel());
            Debug.Log("OnPlayPressed");
        }
    }
}