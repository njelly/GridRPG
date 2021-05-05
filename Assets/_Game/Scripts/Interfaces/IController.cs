using System.Threading.Tasks;

namespace Tofunaut.GridRPG.Interfaces
{
    public interface IController
    {
        public abstract Task Load(IRouter router, object model);
        public abstract Task OnGainedFocus();
        public abstract Task OnLostFocus();
        public abstract Task Unload();
    }
}