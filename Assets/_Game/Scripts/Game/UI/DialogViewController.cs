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
        private int _currentPage = 1;

        public override Task OnPushedToStack(DialogViewModel model)
        {
            _model = model;
            _fadeInGroup.alpha = 0f;
            _fadeInGroup.DOFade(1f, 0.35f);
            
            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(NextDialog);
            
            EventSystem.current.SetSelectedGameObject(_nextButton.gameObject);

            // record the player actor input target, we'll reassign it later if it exists
            _prevPlayerActorInputTarget = GameContext.PlayerActorInputTarget;
            GameContext.SetPlayerActorInputTarget(null);

            _dialogLabel.pageToDisplay = 0;
            _dialogLabel.text = string.Empty;
            
            NextDialog();

            return Task.CompletedTask;
        }

        public override async Task OnPoppedFromStack()
        {
            // wait for the fade animation to complete
            var fadeComplete = false;
            _fadeInGroup.DOFade(0f, 0.35f).OnComplete(() =>
            {
                fadeComplete = true;
            });
            while (!fadeComplete)
                await Task.Yield();
            
            // reassign the previous player actor input target if it still exists
            if(_prevPlayerActorInputTarget)
                GameContext.SetPlayerActorInputTarget(_prevPlayerActorInputTarget);
        }

        private void NextDialog()
        {
            // go to the next page of the TextMeshPro label if there is one
            if (_dialogLabel.pageToDisplay < _dialogLabel.textInfo.pageCount)
            {
                _dialogLabel.pageToDisplay++;
                return;
            }
            
            // invoke on dialog depleted when out of dialogs
            if (_model.DialogQueue.Count <= 0)
            {
                _model.OnDialogDepleted?.Invoke();
                return;
            }
            
            // set the new text on the TextMeshPro label
            var dialog = _model.DialogQueue.Dequeue();
            _dialogLabel.pageToDisplay = 1;
            _dialogLabel.text = dialog.Text;
        }
    }
}