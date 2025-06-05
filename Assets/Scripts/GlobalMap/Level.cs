using System.Collections;
using Managers;
using Player;
using UnityEngine;

namespace GlobalMap
{
    public class Level : MonoBehaviour
    {
        [SerializeField] public string levelName;
        [SerializeField] public bool finished;
        [SerializeField] private Canvas interactCanvas;
        private PlayerInteraction _playerInteraction;

        private IEnumerator Start()
        {

            while (!LevelsManager.Singleton)
            {
                yield return null;
            }
            
            finished = LevelsManager.Singleton.GetFinishedStatus(levelName);
            
            if (finished)
            {
                enabled = false;
            }
            
            if (interactCanvas)
            {
                interactCanvas.enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!enabled) return;
            
            interactCanvas.enabled = true;
            
            _playerInteraction = other.gameObject.GetComponent<PlayerInteraction>();
            if (!_playerInteraction) return;
            
            _playerInteraction.SetInteraction(OnInteract);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!enabled) return;
            
            interactCanvas.enabled = false;
            _playerInteraction.RemoveInteraction();
        }

        private void OnInteract()
        {
            LevelsManager.Singleton.currentLevel = levelName;
            GameManager.Singleton.ChangeScene(levelName);
        }
    }
}