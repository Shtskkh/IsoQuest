using Managers;
using Player;
using UnityEngine;

namespace GlobalMap
{
    public class MoveToLevel : MonoBehaviour
    {
        [SerializeField] private string levelName;
        [SerializeField] private Level level;
        private PlayerInteraction _playerInteraction;

        private void OnTriggerEnter(Collider other)
        {
            if (!enabled) return;
            
            _playerInteraction = other.gameObject.GetComponent<PlayerInteraction>();
            if (!_playerInteraction) return;
           
            _playerInteraction.SetInteraction(OnInteract);
        }

        private void OnTriggerExit(Collider other)
        { 
            if (!enabled) return;
            if (!_playerInteraction) return;
            _playerInteraction.RemoveInteraction();
        }

        private void OnInteract()
        {
            if (!enabled) return;
            GameManager.Singleton.ChangeScene(levelName);
        }
    }
}