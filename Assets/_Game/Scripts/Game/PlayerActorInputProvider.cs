using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tofunaut.GridRPG.Game
{
    public class PlayerActorInputProvider : MonoBehaviour, IActorInputProvider
    {
        public ActorInput Input { get; private set; }

        [SerializeField] private PlayerInput _playerInput;

        private InputAction _moveAction;
        private InputAction _interactAction;
        
        private void Awake()
        {
            Input = new ActorInput();
        }

        private void Start()
        {
            _moveAction = _playerInput.actions.FindAction("Move");
            _moveAction.started += OnMoveAction;
            _moveAction.performed += OnMoveAction;
            _moveAction.canceled += OnMoveAction;

            _interactAction = _playerInput.actions.FindAction("Interact");
            _interactAction.started += OnInteractAction;
            _interactAction.performed += OnInteractAction;
            _interactAction.canceled += OnInteractAction;
        }

        private void OnDestroy()
        {
            _moveAction.started -= OnMoveAction;
            _moveAction.performed -= OnMoveAction;
            _moveAction.canceled -= OnMoveAction;
            
            _interactAction.started -= OnInteractAction;
            _interactAction.performed -= OnInteractAction;
            _interactAction.canceled -= OnInteractAction;
        }

        private void OnMoveAction(InputAction.CallbackContext context)
        {
            Input.Direction.Axis = context.canceled ? Vector2.zero : context.ReadValue<Vector2>();
        }

        private void OnInteractAction(InputAction.CallbackContext context)
        {
            if (context.started)
                Input.Interacting.IsPressed = true;
            else if (context.canceled)
                Input.Interacting.IsPressed = false;
        }
    }
}