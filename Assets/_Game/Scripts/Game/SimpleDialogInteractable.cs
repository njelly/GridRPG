using System.Collections.Generic;
using Tofunaut.GridRPG.Game.UI;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class SimpleDialogInteractable : Interactable
    {
        [SerializeField] private string _dialog;
        
        public override async void OnBeginInteraction(Actor interactor)
        {
            var dialogView = default(DialogViewController);
            var dialogViewModel = new DialogViewModel
            {
                DialogQueue = new Queue<Dialog>(new []
                {
                    new Dialog
                    {
                        Text = _dialog,
                    },
                }),
                OnDialogDepleted = async () =>
                {
                    await AppContext.ViewStack.PopUpTo(dialogView);
                }
            };

            dialogView = await AppContext.ViewStack.Push<DialogViewController, DialogViewModel>(AppConstants.AssetPaths.UI.DialogView, dialogViewModel);
        }

        public override void OnEndInteraction(Actor interactor)
        {
        }
    }
}