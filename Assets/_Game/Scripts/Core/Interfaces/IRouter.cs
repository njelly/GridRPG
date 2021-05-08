using System.Threading.Tasks;

namespace Tofunaut.Core.Interfaces
{
    public interface IRouter
    {
        IContext Context { get; }
        
        Task<TController> GoTo<TController, TControllerModel>(TControllerModel model)
            where TController : Controller<TControllerModel>;

        Task<IController> Back();
        Task ClearHistory();

        Task<TController> ReplaceCurrent<TController, TControllerModel>(TControllerModel model)
            where TController : Controller<TControllerModel>;
    }
}