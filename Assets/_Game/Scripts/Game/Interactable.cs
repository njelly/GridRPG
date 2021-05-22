using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void OnBeginInteraction(Actor interactor);
        public abstract void OnEndInteraction(Actor interactor);
    }
}