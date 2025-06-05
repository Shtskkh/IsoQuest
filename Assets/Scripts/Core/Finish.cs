using Managers;
using Player;
using UnityEngine;

namespace Core
{
    public class Finish : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerMovement>()) return;
            GameManager.Singleton.paymentAbility += 5;
            GameManager.Singleton.ChangeScene("GlobalMap");
            LevelsManager.Singleton.SetFinishedStatus(LevelsManager.Singleton.currentLevel);
        }
    }
}