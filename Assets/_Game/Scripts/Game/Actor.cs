using System;
using System.Linq;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class Actor : MonoBehaviour
    {
        public ActorInput Input => _actorInputProvider?.Input ?? new ActorInput();
        public Vector2Int Coord => new Vector2Int(Mathf.RoundToInt(_t.position.x), Mathf.RoundToInt(_t.position.y));
        
        private IActorInputProvider _actorInputProvider;
        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }

        public void SetActorInputProvider(IActorInputProvider provider)
        {
            if (provider == null)
            {
                _actorInputProvider = (IActorInputProvider) GetComponent(typeof(IActorInputProvider)) ??
                                      new NoOpActorInputProvider();
            }
            else
            {
                _actorInputProvider = provider;
            }
        }
    }
}