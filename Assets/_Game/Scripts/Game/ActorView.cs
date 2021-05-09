using Tofunaut.GridRPG.Game.ActorComponents;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class ActorView : MonoBehaviour
    {
        public Actor Actor { get; private set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Facing _actorFacing;

        private void Awake()
        {
            Actor = GetComponentInParent<Actor>();
            _actorFacing = GetComponentInParent<Facing>();
        }

        private void Update()
        {
            if (_actorFacing)
            {
                _spriteRenderer.flipX = _actorFacing.Direction switch
                {
                    Facing.EDirection.East => false,
                    Facing.EDirection.West => true,
                    _ => _spriteRenderer.flipX
                };
            }
        }
    }
}