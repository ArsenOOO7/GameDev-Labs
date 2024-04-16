using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class GlobalEventManager : MonoBehaviour
    {
        public static readonly UnityEvent<float> OnPlayerFinished = new();
        public static readonly UnityEvent OnPlayerDeath = new();
        public static readonly UnityEvent<float> OnUpdateWaitingTime = new();

        public static void PlayerFinished(float totalGameTime)
        {
            OnPlayerFinished.Invoke(totalGameTime);
        }

        public static void PlayerDeath()
        {
            OnPlayerDeath.Invoke();
        }

        public static void UpdateWaitingTime(float waitingTime)
        {
            OnUpdateWaitingTime.Invoke(waitingTime);
        }
    }
}