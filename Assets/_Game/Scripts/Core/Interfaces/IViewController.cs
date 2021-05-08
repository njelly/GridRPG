using System.Threading.Tasks;

namespace Tofunaut.Core.Interfaces
{
    public interface IViewController
    {
        public abstract Task Initialize(object model);
        public abstract Task OnGainedFocus();
        public abstract Task OnLostFocus();
    }
}