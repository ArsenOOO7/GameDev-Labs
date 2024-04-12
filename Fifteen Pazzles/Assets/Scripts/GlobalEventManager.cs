using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class GlobalEventManager : MonoBehaviour
    {
        public static readonly UnityEvent OnGameFinish = new();

        public static void Finish()
        {
            OnGameFinish.Invoke();
        }
    }
}