using System;
using Game.Event;
using TMPro;
using UnityEngine;

namespace Game.Level
{
    public class HudController : MonoBehaviour
    {
        
        public static HudController Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private TextMeshProUGUI enemiesText;
        [SerializeField] private TextMeshProUGUI chestsText;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            EntityEventManager.OnChestOpen.AddListener(chests => chestsText.SetText("Opened chests: " + chests));
            EntityEventManager.OnHpChange.AddListener(health => hpText.SetText("HP: " + health.ToString("F1")));
            EntityEventManager.OnEnemyCountChange.AddListener(enemies => enemiesText.SetText("Enemies: " + enemies));
        }

        public void UpdateLevel(int level)
        {
            levelText.SetText("Level: " + level);
        }
    }
}