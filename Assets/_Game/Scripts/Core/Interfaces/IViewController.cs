using System.Threading.Tasks;

namespace Tofunaut.Core.Interfaces
{
    public interface IViewController
    {
        Task OnPushedToStack(object model);
        Task OnPoppedFromStack();
        Task OnGainedFocus();
        Task OnLostFocus();
    }
}