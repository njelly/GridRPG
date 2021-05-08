using System.Threading.Tasks;

namespace Tofunaut.Core.Interfaces
{
    public interface IController
    {
        public abstract Task Load(IRouter router, object model);
        public abstract Task OnGainedFocus();
        public abstract Task OnLostFocus();
        public abstract Task Unload();
    }
}