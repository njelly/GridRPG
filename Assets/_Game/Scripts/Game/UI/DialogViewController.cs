using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Tofunaut.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        [SerializeField] private Button _nextButton;

        private DialogViewModel _model;
        private Actor _prevPlayerActorInputTarget;

        public override Task OnPushedToStack(DialogViewModel model)
        {
            _model = model;
            _fadeInGroup.DOFade(1f, 0.35f);
            
            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(NextDialog);
            
            EventSystem.current.SetSelectedGameObject(_nextButton.gameObject);

            // record the player actor input target, we'll reassign it later if it exists
            _prevPlayerActorInputTarget = GameContext.PlayerActorInputTarget;
            GameContext.SetPlayerActorInputTarget(null);
            
            NextDialog();

            return Task.CompletedTask;
        }

        public override Task OnPoppedFromStack()
        {
            Debug.Log("popped from stack");
            
            // reassign the previous player actor input target if it still exists
            if(_prevPlayerActorInputTarget)
                GameContext.SetPlayerActorInputTarget(_prevPlayerActorInputTarget);

            return Task.CompletedTask;
        }

        private void NextDialog()
        {
            Debug.Log("next dialog");
            
            if (_model.DialogQueue.Count <= 0)
            {
                _model.OnDialogDepleted?.Invoke();
                return;
            }
            
            var dialog = _model.DialogQueue.Dequeue();
            _dialogLabel.text = dialog.Text;
        }
    }
}