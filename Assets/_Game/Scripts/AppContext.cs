using Tofunaut.Core;
using Tofunaut.Core.Interfaces;
using Tofunaut.GridRPG.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tofunaut.GridRPG
{
    public class AppContext : MonoBehaviour
    {
        public IViewService ViewService { get; private set; }
        public MapCatalog MapCatalog => _mapCatalog;
        public AssetReference GameContextAssetReference => _gameContextAssetReference;
        
        [SerializeField] private Canvas _canvas;
        [SerializeField] private MapCatalog _mapCatalog;
        [SerializeField] private AssetReference _gameContextAssetReference;
        
        private IRouter _router;

        private async void Start()
        {
            ViewService = new ViewService(_canvas);
            
            _router = new Router();
            
            await _router.GoTo<StartScreenController, StartScreenModel>(new StartScreenModel
            {
                AppContext = this,
            });
        }
    }
}