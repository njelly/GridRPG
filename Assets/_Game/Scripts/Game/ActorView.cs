using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class ActorView : MonoBehaviour
    {
        public Actor Actor { get; private set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            Actor = GetComponentInParent<Actor>();
        }

        private void Update()
        {
            _spriteRenderer.flipX = false;
        }
    }
}