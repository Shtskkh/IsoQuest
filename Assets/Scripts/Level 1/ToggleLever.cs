using Player;
using UnityEngine;

namespace Level_1
{
    public class ToggleLever : MonoBehaviour
    {
        [SerializeField] private GameObject leverHandle;
        [SerializeField] private float deflectionAngle;
        [SerializeField] private bool toggled;

        private void Awake()
        {
            leverHandle.transform.rotation = toggled
                ? Quaternion.Euler(0, 0, deflectionAngle)
                : Quaternion.Euler(0, 0, -deflectionAngle);
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