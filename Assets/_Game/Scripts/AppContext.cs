using Tofunaut.GridRPG.Interfaces;
using UnityEngine;

namespace Tofunaut.GridRPG
{
    public class AppContext : MonoBehaviour
    {
        private IRouter _router;

        private async void Start()
        {
            _router = ScriptableObject.CreateInstance<Router>();
            await _router.GoTo<StartScreenController, StartScreenModel>(new StartScreenModel());
        }
    }
}