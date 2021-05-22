using UnityEditor.SearchService;
using UnityEngine;

namespace Tofunaut.GridRPG.Game.ActorComponents
{
    [RequireComponent(typeof(Facing))]
    public class Interactor : ActorComponent
    {
        public bool IsInteracting => _interactingWith;
        
        private bool _wasInteracting;
        private Interactable _interactingWith;
        private Facing _facing;

        protected override void Awake()
        {
            base.Awake();
            _facing = GetComponent<Facing>();
        }
        
        private void Update()
        {
            var isInteracting = Actor.Input.Interacting;
            if (!_wasInteracting && isInteracting)
            {
                BeginInteracting();
            }
            else if(_wasInteracting && !isInteracting)
            {
                EndInteracting();
            }
        }

        private void BeginInteracting()
        {
            _wasInteracting = true;

            var testCoord = Actor.Coord + Facing.DirectionToVector2(_facing.Direction);
            var results = new Collider2D[1];
            var numResults = Physics2D.OverlapCircleNonAlloc(testCoord, 0.5f - float.Epsilon, results);
            if (numResults <= 0)
                return;

            _interactingWith = results[0].GetComponent<Interactable>();
            if (!_interactingWith)
                return;
            
            _interactingWith.OnBeginInteraction(Actor);
        }

        private void EndInteracting()
        {
            _wasInteracting = false;
            
            if(_interactingWith)
                _interactingWith.OnEndInteraction(Actor);

            _interactingWith = null;
        }
    }
}