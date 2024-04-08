using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UIEventManager : MonoBehaviour
    {
        public static readonly UnityEvent<float> OnScoreChange = new();

        public static void ChangeScore(float newScore)
        {
            OnScoreChange.Invoke(newScore);
        }
    }
}