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
        public override Task Load(IRouter router, StartScreenModel model)
        {
            Debug.Log("Load");
            return Task.CompletedTask;
        }

        public override Task OnGainedFocus()
        {
            Debug.Log("OnGainedFocus");
            return Task.CompletedTask;
        }

        public override Task OnLostFocus()
        {
            Debug.Log("OnLostFocus");
            return Task.CompletedTask;
        }

        public override Task Unload()
        {
            Debug.Log("Unload");
            return Task.CompletedTask;
        }
    }
}