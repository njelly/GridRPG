using System.Threading.Tasks;
using Tofunaut.Core.Interfaces;
using UnityEngine;

namespace Tofunaut.Core
{
    public abstract class Controller<TControllerModel> : ScriptableObject, IController
    {
        public async Task Load(IRouter router, object model) =>  await Load(router, (TControllerModel) model);
        public abstract Task Load(IRouter router, TControllerModel model);

        public virtual Task OnGainedFocus()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnLostFocus()
        {
            return Task.CompletedTask;
        }
        
        public virtual Task Unload()
        {
            return Task.CompletedTask;
        }
    }
}