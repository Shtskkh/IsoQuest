using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Level_1
{
    public class Lever : MonoBehaviour
    {
        [SerializeField] private GameObject leverHandle;
        [SerializeField] private float deflectionAngle;
        [SerializeField] private bool toggled;

        [SerializeField] private GameObject platform1;
        [SerializeField] private GameObject platform2;
        
        [SerializeField] private Canvas uiCanvas;

        private InputAction _interact;
        private bool _canInteract;
    
        private void Awake()
        {
            _interact = InputSystem.actions.FindAction("Interact");
            uiCanvas.enabled = false;
            leverHandle.transform.rotation = toggled
                ? Quaternion.Euler(0, 0, deflectionAngle)
                : Quaternion.Euler(0, 0, -deflectionAngle);
        }

        private void Update()
        {
            if (!_canInteract) return;
            if (_interact.WasPerformedThisFrame())
            {
                Toggle();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerMovement>()) return;
            uiCanvas.enabled = true;
            _canInteract = true;
        }

        private void OnTriggerExit(Collider other)
        {
            uiCanvas.enabled = false;
            _canInteract = false;
        }

        private void Toggle()
        {
            toggled = !toggled;
            leverHandle.transform.rotation = toggled
                ? Quaternion.Euler(0, 0, deflectionAngle)
                : Quaternion.Euler(0, 0, -deflectionAngle);
        }
    }
}
