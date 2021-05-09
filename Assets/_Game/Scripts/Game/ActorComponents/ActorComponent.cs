using UnityEngine;

namespace Tofunaut.GridRPG.Game.ActorComponents
{
    [RequireComponent(typeof(Actor))]
    public abstract class ActorComponent : MonoBehaviour
    {
        public Actor Actor { get; private set; }
        
        protected virtual void Awake()
        {
            Actor = GetComponent<Actor>();
        }
    }
}