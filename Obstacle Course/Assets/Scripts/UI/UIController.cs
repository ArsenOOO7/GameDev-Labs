using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel, lostPanel;

        [SerializeField] private TextMeshProUGUI waitingTime;

        private void Start()
        {
            GlobalEventManager.OnPlayerFinished.AddListener(() => winPanel.SetActive(true));
            GlobalEventManager.OnPlayerDeath.AddListener(() => lostPanel.SetActive(true));
            GlobalEventManager.OnUpdateWaitingTime.AddListener((wt) => waitingTime.SetText("Time: " + wt.ToString("F1")));
        }
    }
}