using Managers;
using Player;
using UnityEngine;

namespace Core
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private int addPaymentAbility;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerMovement>()) return;
            GameManager.Singleton.paymentAbility += addPaymentAbility;
            GameManager.Singleton.ChangeScene("GlobalMap");
            LevelsManager.Singleton.SetFinishedStatus(LevelsManager.Singleton.currentLevel);
        }
    }
}