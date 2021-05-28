using Tofunaut.Core;
using Tofunaut.Core.Interfaces;
using Tofunaut.GridRPG.Data;
using Tofunaut.GridRPG.Game;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tofunaut.GridRPG
{
    public class AppContext : SingletonBehaviour<AppContext>
    {
        public static ViewStack  ViewStack => _instance._viewStack;
        public static MapCatalog MapCatalog => _instance._mapCatalog;
        public static IRouter Router => _instance._router;
        
        [SerializeField] private Canvas _canvas;
        [SerializeField] private MapCatalog _mapCatalog;
        
        private IRouter _router;
        private ViewStack _viewStack;

        protected override void Awake()
        {
            base.Awake();
            
            _viewStack = new ViewStack(_canvas);
            _router = new Router();
        }

        private async void Start()
        {
            await _router.GoTo<StartScreenController, StartScreenModel>(new StartScreenModel());
        }
    }
}