using Managers;
using UnityEngine;

namespace Core
{
    public class Fail : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Singleton.ReloadScene();
            }
        }
    }
}
