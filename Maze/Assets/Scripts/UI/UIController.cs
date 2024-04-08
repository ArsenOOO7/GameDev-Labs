using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        private static readonly String ScoreTemplate = "Points: ";

        [SerializeField] private TextMeshProUGUI scoreText;

        private void Start()
        {
            UIEventManager.OnScoreChange.AddListener(OnChange);
        }

        private void OnChange(float newScore)
        {
            scoreText.SetText(ScoreTemplate + newScore.ToString("F1"));
        }
    }
}