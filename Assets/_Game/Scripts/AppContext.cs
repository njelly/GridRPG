using Tofunaut.Core;
using Tofunaut.Core.Interfaces;
using UnityEngine;

namespace Tofunaut.GridRPG
{
    public class AppContext : MonoBehaviour
    {
        private static AppContext _instance;
        
        private IRouter _router;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(this);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private async void Start()
        {
            _router = ScriptableObject.CreateInstance<Router>();
            await _router.GoTo<StartScreenController, StartScreenModel>(new StartScreenModel());
        }
    }
}