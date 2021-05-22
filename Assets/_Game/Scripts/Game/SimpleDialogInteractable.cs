using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class SimpleDialogInteractable : Interactable
    {
        [SerializeField] private string _dialog;
        
        public override void OnBeginInteraction(Actor interactor)
        {
            Debug.Log(_dialog);
        }

        public override void OnEndInteraction(Actor interactor)
        {
            Debug.Log("Good bye!");
        }
    }
}