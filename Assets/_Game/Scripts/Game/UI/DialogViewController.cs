using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Tofunaut.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tofunaut.GridRPG.Game.UI
{
    public class DialogViewModel
    {
        public Queue<Dialog> DialogQueue;
        public Action OnDialogDepleted;
    }

    public class Dialog
    {
        public string Text;
    }
    
    public class DialogViewController : ViewController<DialogViewModel>
    {
        public bool IsShowing => gameObject.activeInHierarchy;
        
        [SerializeField] private TextMeshProUGUI _dialogLabel;
        [SerializeField] private CanvasGroup _fadeInGroup;

        private DialogViewModel _model;
        private Queue<Dialog> _dialogQueue;
        private Actor _prevPlayerActorInputTarget;

        public override Task OnPushedToStack(DialogViewModel model)
        {
            _model = model;
            _dialogQueue = new Queue<Dialog>();
            _fadeInGroup.DOFade(1f, 0.35f);

            // record the player actor input target, we'll reassign it later if it exists
            _prevPlayerActorInputTarget = GameContext.PlayerActorInputTarget;
            GameContext.SetPlayerActorInputTarget(null);

            return Task.CompletedTask;
        }

        public override Task OnPoppedFromStack()
        {
            // reassign the previous player actor input target if it still exists
            if(_prevPlayerActorInputTarget)
                GameContext.SetPlayerActorInputTarget(_prevPlayerActorInputTarget);

            return Task.CompletedTask;
        }

        private void NextDialog()
        {
            if (_dialogQueue.Count <= 0)
            {
                _model.OnDialogDepleted?.Invoke();
                return;
            }
            
            var dialog = _dialogQueue.Dequeue();
            _dialogLabel.text = dialog.Text;
        }
    }
}