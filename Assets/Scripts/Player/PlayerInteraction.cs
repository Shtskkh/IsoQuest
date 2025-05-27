using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    { 
        [SerializeField] private GameObject interactButton;
        private readonly UnityEvent _interactionEvent = new();
        private InputAction _interactInput;

        private void Awake()
        {
            _interactInput = InputSystem.actions.FindAction("Interact");
        }

        private void Start()
        {
            interactButton.SetActive(false);
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

            interactButton.SetActive(true);

        }

        public void RemoveInteraction()
        {
            _interactionEvent.RemoveAllListeners();
            interactButton.SetActive(false);
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
