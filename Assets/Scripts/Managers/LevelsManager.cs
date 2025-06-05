using System;
using System.Collections.Generic;
using GlobalMap;
using UnityEngine;

namespace Managers
{
    public class LevelsManager : MonoBehaviour
    {
        public static LevelsManager Singleton { get; private set; }
        
        public string currentLevel;
        
        private readonly Dictionary<string, Level> _allLevels = new();

        private void Awake()
        {
            if (!Singleton)
            {
                Singleton = this;
                DontDestroyOnLoad(gameObject);
            }

            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            foreach (var level in FindObjectsByType<Level>(FindObjectsSortMode.InstanceID))
            {
                _allLevels.Add(level.levelName, level);
            }
        }

        public bool GetFinishedStatus(string levelName)
        {
            if (!_allLevels.TryGetValue(levelName, out var level))
            {
                throw new ArgumentException("Level not found");
            }
            
            return level.finished;
        }

        public void SetFinishedStatus(string levelName)
        {
            if (!_allLevels.TryGetValue(levelName, out var level))
            {
                throw new ArgumentException("Level not found");
            }
            
            level.finished = true;
        }
    }
}