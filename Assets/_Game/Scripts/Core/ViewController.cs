using System.Threading.Tasks;
using Tofunaut.Core.Interfaces;
using UnityEngine;

namespace Tofunaut.Core
{
    public abstract class ViewController<TControllerModel> : MonoBehaviour, IViewController
    {
        public async Task Initialize(object model) =>  await Initialize((TControllerModel) model);
        public abstract Task Initialize(TControllerModel model);

        public virtual Task OnGainedFocus()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnLostFocus()
        {
            return Task.CompletedTask;
        }
    }
}