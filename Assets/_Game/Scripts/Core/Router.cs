using System.Collections.Generic;
using System.Threading.Tasks;
using Tofunaut.Core.Interfaces;
using UnityEngine;

namespace Tofunaut.Core
{
    public class Router : IRouter
    {
        private readonly Stack<IController> _controllerStack;

        public Router()
        {
            _controllerStack = new Stack<IController>();
        }
        
        public async Task<TController> GoTo<TController, TControllerModel>(TControllerModel model) where TController : Controller<TControllerModel>
        {
            // lose focus for the current controller
            if (_controllerStack.Count > 0)
                await _controllerStack.Peek().OnLostFocus();
            
            // push, load, and gain focus for the new controller
            var controller = ScriptableObject.CreateInstance<TController>();
            _controllerStack.Push(controller);
            await controller.Load(this, model);
            await controller.OnGainedFocus();

            return controller;
        }

        public async Task<IController> Back()
        {
            if (_controllerStack.Count <= 0)
                return null;

            // lose focus and unload the current controller
            var currentController = _controllerStack.Pop();
            await currentController.OnLostFocus();
            await currentController.Unload();
            
            // return if there are no more controllers
            if (_controllerStack.Count <= 0)
                return null;

            // gain focus for the next controller
            currentController = _controllerStack.Peek();
            await currentController.OnGainedFocus();
            return currentController;
        }

        public async Task ClearHistory()
        {
            // pop the current controller and unload all other controllers
            var currentController = _controllerStack.Pop();
            foreach (var controller in _controllerStack)
                await controller.Unload();
            
            _controllerStack.Clear();
            _controllerStack.Push(currentController);
        }        
        
        public async Task<TController> ReplaceCurrent<TController, TControllerModel>(TControllerModel model) where TController : Controller<TControllerModel>
        {
            // lose focus and unload the current controller
            var currentController = _controllerStack.Pop();
            await currentController.OnLostFocus();
            await currentController.Unload();
            
            // push, load, and gain focus for the new controller
            var controller = ScriptableObject.CreateInstance<TController>();
            _controllerStack.Push(controller);
            await controller.Load(this, model);
            await controller.OnGainedFocus();

            return controller;
        }
    }
}