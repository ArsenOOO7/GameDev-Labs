using System;
using UnityEngine;

namespace Game.Level
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Instance { get; private set; }

        private int _currentLevel;
        
        private void Awake()
        {
            Instance = this;
            _currentLevel = 1;
        }

        public void ChangeLevel()
        {
            HudController.Instance.UpdateLevel(++_currentLevel);
        }
    }
}