using Event;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speedText, crashesText;
        [SerializeField] private GameObject finishPanel;

        private void Start()
        {
            finishPanel.SetActive(false);
            RoadEventManager.OnPlayerSpeedChanges.AddListener(playerSpeed =>
            {
                speedText.SetText("Speed: " + playerSpeed.ToString("F1"));
            });
            RoadEventManager.OnPlayerCrash.AddListener(crashes =>
            {
                crashesText.SetText("Crashes: " + crashes + "/5");
            });
            GlobalEventManager.OnGameStop.AddListener(() =>
            {
                finishPanel.SetActive(true);
            });
        }
    }
}