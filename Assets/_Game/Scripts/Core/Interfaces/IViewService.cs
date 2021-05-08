using System.Threading.Tasks;

namespace Tofunaut.Core.Interfaces
{
    public interface IViewService
    {
        IViewController Current { get; }
        
        Task<TViewController> Push<TViewController, TViewControllerModel>(TViewControllerModel model)
            where TViewController : ViewController<TViewControllerModel>;

        Task<IViewController> Pop();
        
        Task ClearHistory();

        Task<TController> ReplaceCurrent<TController, TControllerModel>(TControllerModel model)
            where TController : ViewController<TControllerModel>;
    }
}