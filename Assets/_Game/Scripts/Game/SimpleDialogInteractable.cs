using Tofunaut.GridRPG.Game.UI;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class SimpleDialogInteractable : Interactable
    {
        [SerializeField] private string _dialog;
        
        public override void OnBeginInteraction(Actor interactor)
        {
            GameContext.DialogView.QueueDialog(new Dialog
            {
                text = _dialog,
            });
        }

        public override void OnEndInteraction(Actor interactor)
        {
        }
    }
}