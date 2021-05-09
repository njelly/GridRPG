using Tofunaut.Core;
using Tofunaut.Core.Interfaces;
using UnityEngine;

namespace Tofunaut.GridRPG
{
    public class AppContext : MonoBehaviour, IContext
    {
        public IViewService ViewService { get; private set; }
        
        [SerializeField] private Canvas _canvas;
        
        private IRouter _router;

        private async void Start()
        {
            ViewService = new ViewService(_canvas);
            
            _router = new Router(this);
            
            await _router.GoTo<StartScreenController, StartScreenModel>(new StartScreenModel());
        }
    }
}