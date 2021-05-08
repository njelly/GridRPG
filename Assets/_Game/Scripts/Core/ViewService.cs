using System.Collections.Generic;
using System.Threading.Tasks;
using Tofunaut.Core.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tofunaut.Core
{
    public class ViewService : IViewService
    {
        public IViewController Current => _viewControllerStack.Count > 0 ? _viewControllerStack.Peek() : null;
        
        private readonly Canvas _canvas;
        private readonly Stack<IViewController> _viewControllerStack;

        public ViewService(Canvas canvas)
        {
            _canvas = canvas;
            _viewControllerStack = new Stack<IViewController>();
        }

        public async Task<TViewController> Push<TViewController, TViewControllerModel>(TViewControllerModel model) where TViewController : ViewController<TViewControllerModel>
        {
            // load the next view controller
            var viewController = (await Addressables.InstantiateAsync(typeof(TViewController).FullName, _canvas.transform, false).Task)
                .GetComponent<ViewController<TViewControllerModel>>();
            _viewControllerStack.Push(viewController);

            //  lose focus on the current ViewController (if it exists!)
            var currentViewController = _viewControllerStack.Peek();
            if (currentViewController != null)
                await currentViewController.OnLostFocus();
            
            // initialize and gain focus on the new ViewController
            await viewController.Initialize(model);
            await viewController.OnGainedFocus();
            
            return viewController as TViewController;
        }

        public async Task<IViewController> Pop()
        {
            if (_viewControllerStack.Count <= 0)
                return null;

            // lose focus on the current ViewController and destroy the GameObject (if it exists)
            var currentViewController = _viewControllerStack.Pop();
            await currentViewController.OnLostFocus();
            if(currentViewController is MonoBehaviour currentViewControllerBehaviour)
                Object.Destroy(currentViewControllerBehaviour.gameObject);
            
            if (_viewControllerStack.Count <= 0)
                return null;

            // gain focus on the previous ViewController
            currentViewController = _viewControllerStack.Peek();
            await currentViewController.OnGainedFocus();
            return currentViewController;
        }

        public Task ClearHistory()
        {
            throw new System.NotImplementedException();
        }

        public Task<TController> ReplaceCurrent<TController, TControllerModel>(TControllerModel model) where TController : ViewController<TControllerModel>
        {
            throw new System.NotImplementedException();
        }
    }
}