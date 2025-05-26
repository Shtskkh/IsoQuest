using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    { 
        private readonly UnityEvent _interactionEvent = new();
        private InputAction _interactInput;

        private void Awake()
        {
            _interactInput = InputSystem.actions.FindAction("Interact");
        }
        
        private void Update()
        {
            if (_interactInput.WasPerformedThisFrame())
                _interactionEvent.Invoke();
        }

        public void SetInteraction(UnityAction interactionEvent)
        {
            _interactionEvent.RemoveAllListeners();
            _interactionEvent.AddListener(interactionEvent.Invoke);
        }

        public void RemoveInteraction()
        {
            _interactionEvent.RemoveAllListeners();
        }

        private void OnEnable()
        {
            _interactInput.Enable();
        }

        private void OnDisable()
        {
            _interactInput.Disable();
        }
    }
}
