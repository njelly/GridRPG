using System;
using System.Linq;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class Actor : MonoBehaviour
    {
        public ActorInput Input => _actorInputProvider.Input;
        public Vector2Int Coord => new Vector2Int(Mathf.RoundToInt(_t.position.x), Mathf.RoundToInt(_t.position.y));
        
        private IActorInputProvider _actorInputProvider;
        private Transform _t;

        private void Awake()
        {
            _actorInputProvider = (IActorInputProvider) GetComponent(typeof(IActorInputProvider)) ?? new NoOpActorInputProvider();
            _t = transform;
        }
    }
}