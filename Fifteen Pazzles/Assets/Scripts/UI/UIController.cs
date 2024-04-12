using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class UIController : MonoBehaviour
    {
        private static readonly string MOVES = "Moves: ";

        [SerializeField] private TextMeshProUGUI movesText;
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject game; 

        private void Start()
        {
            panel.SetActive(false);
            UIEventManager.ON_MOVEMENENT.AddListener((moves) => { movesText.SetText(MOVES + moves); });
            GlobalEventManager.OnGameFinish.AddListener(() =>
            {
                movesText.gameObject.SetActive(false);
                game.SetActive(false);
                panel.SetActive(true);
            });
        }
    }
}