using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        private static readonly String ScoreTemplate = "Points: ";

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject panel;

        private void Start()
        {
            panel.SetActive(false);
            UIEventManager.OnScoreChange.AddListener(OnChange);
            GlobalEventManager.OnGameFinish.AddListener(OnFinish);
        }

        private void OnChange(float newScore)
        {
            scoreText.SetText(ScoreTemplate + newScore.ToString("F1"));
        }

        private void OnFinish()
        {
            panel.SetActive(true);
        }
    }
}