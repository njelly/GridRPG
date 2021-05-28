using System.Threading.Tasks;
using Tofunaut.Core.Interfaces;
using UnityEngine;

namespace Tofunaut.Core
{
    public abstract class ViewController<TViewModel> : MonoBehaviour, IViewController
    {
        public Task OnPushedToStack(object model) => OnPushedToStack((TViewModel) model);
        public abstract Task OnPushedToStack(TViewModel model);

        public virtual Task OnPoppedFromStack()
        {
            return Task.CompletedTask;
        }

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