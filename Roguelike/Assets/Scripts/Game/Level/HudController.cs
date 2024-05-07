using System;
using TMPro;
using UnityEngine;

namespace Game.Level
{
    public class HudController : MonoBehaviour
    {
        
        public static HudController Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI levelText;
        
        private void Awake()
        {
            Instance = this;
        }

        public void UpdateLevel(int level)
        {
            levelText.SetText("Level: " + level);
        }
    }
}