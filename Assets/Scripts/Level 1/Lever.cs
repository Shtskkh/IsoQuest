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
    
        private void Awake()
        {
            uiCanvas.enabled = false;
            leverHandle.transform.rotation = toggled
                ? Quaternion.Euler(0, 0, deflectionAngle)
                : Quaternion.Euler(0, 0, -deflectionAngle);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerMovement>()) return;
            uiCanvas.enabled = true;
            other.GetComponent<PlayerInteraction>().SetInteraction(Toggle);
        }

        private void OnTriggerExit(Collider other)
        {
            uiCanvas.enabled = false;
            other.GetComponent<PlayerInteraction>().RemoveInteraction();
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
