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
        public override async Task Load(IRouter router, StartScreenModel model)
        {
            var startScreenViewModel = new StartScreenViewModel
            {
                OnPlayPressed = OnPlayPressed,
            };

            await router.Context.ViewService.Push<StartScreenView, StartScreenViewModel>(startScreenViewModel);
        }

        private void OnPlayPressed()
        {
            Debug.Log("hey");
        }
    }
}