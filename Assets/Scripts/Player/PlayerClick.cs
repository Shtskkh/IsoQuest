using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerClick : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector2> onClick;

        private InputAction _click;

        private void Awake()
        {
            _click = InputSystem.actions.FindAction("Click");
        }

        private void OnEnable()
        {
            _click.Enable();
            _click.performed += HandleClick;
        }

        private void OnDisable()
        {
            _click.performed -= HandleClick;
            _click.Disable();
        }

        private void HandleClick(InputAction.CallbackContext context)
        {
            Vector2 inputPosition;

            if (Mouse.current != null && context.control.device == Mouse.current)
                inputPosition = Mouse.current.position.ReadValue();

            else if (Touchscreen.current != null && context.control.device == Touchscreen.current)
                inputPosition = Touchscreen.current.position.ReadValue();

            else return;

            onClick.Invoke(inputPosition);
        }
    }
}