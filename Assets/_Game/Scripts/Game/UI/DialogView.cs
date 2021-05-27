using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tofunaut.GridRPG.Game.UI
{
    public class Dialog
    {
        public string text;
    }
    
    public class DialogView : MonoBehaviour
    {
        public bool IsShowing => gameObject.activeInHierarchy;
        
        [SerializeField] private TextMeshProUGUI _dialogLabel;
        [SerializeField] private CanvasGroup _fadeInGroup;
        
        private Queue<Dialog> _dialogQueue;

        private void Awake()
        {
            _dialogQueue = new Queue<Dialog>();
        }

        public void QueueDialog(Dialog dialog)
        {
            _dialogQueue.Enqueue(dialog);
            if (!IsShowing)
                Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
            _fadeInGroup.DOFade(1f, 0.35f);
            GameContext.SetPlayerActorInputTarget(null);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            GameContext.SetPlayerActorInputTarget(GameContext.PlayerActor);
        }

        private void NextDialog()
        {
            if (_dialogQueue.Count <= 0)
                Hide();
            
            var dialog = _dialogQueue.Dequeue();
            _dialogLabel.text = dialog.text;
        }
    }
}