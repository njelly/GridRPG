using System.Collections.Generic;
using System.Threading.Tasks;
using Tofunaut.Core.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tofunaut.Core
{
    public class ViewStack
    {
        private readonly Canvas _canvas;
        private readonly Stack<IViewController> _viewControllerStack;

        public ViewStack(Canvas canvas)
        {
            _canvas = canvas;
            _viewControllerStack = new Stack<IViewController>();
        }

        public async Task<TViewController> Push<TViewController, TViewModel>(string path, TViewModel model) where TViewController : ViewController<TViewModel>
        {
            // load the next view controller
            var viewController = (await Addressables.InstantiateAsync(path, _canvas.transform).Task)
                .GetComponent<ViewController<TViewModel>>();
            _viewControllerStack.Push(viewController);

            //  lose focus on the current ViewController (if it exists!)
            var currentViewController = _viewControllerStack.Peek();
            if (currentViewController != null)
                await currentViewController.OnLostFocus();
            
            // initialize and gain focus on the new ViewController
            await viewController.OnPushedToStack(model);
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

        public async Task PopUntil(IViewController viewController)
        {
            while (_viewControllerStack.Count > 0 && _viewControllerStack.Peek() != viewController)
                await Pop();
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