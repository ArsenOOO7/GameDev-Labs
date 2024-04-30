using UnityEngine.Events;

namespace Event
{
    public static class GlobalEventManager
    {
        public static readonly UnityEvent OnGameStop = new();

        public static void GameStop()
        {
            OnGameStop.Invoke();
        }
    }
}