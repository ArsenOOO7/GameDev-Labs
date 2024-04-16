using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel, lostPanel;

        [SerializeField] private TextMeshProUGUI waitingTime;
        [SerializeField] private TextMeshProUGUI totalTime;

        private void Start()
        {
            GlobalEventManager.OnPlayerFinished.AddListener((totalGameTime) => { 
                winPanel.SetActive(true);
                totalTime.SetText("Congrats! You're the best!\nTotal time: " + totalGameTime.ToString("F1"));
            });
            GlobalEventManager.OnPlayerDeath.AddListener(() => lostPanel.SetActive(true));
            GlobalEventManager.OnUpdateWaitingTime.AddListener((wt) => waitingTime.SetText("Time: " + wt.ToString("F1")));
        }
    }
}