using Game.Utils;
using UnityEngine.Events;

namespace Game.Event
{
    public static class GlobalEventManager
    {
        public static readonly UnityEvent OnTick = new();
        public static readonly UnityEvent OnGameStop = new();
        public static readonly UnityEvent OnGameStart = new();

        public static void Tick()
        {
            if (GameStateManager.IsStopped)
            {
                return;
            }
            OnTick.Invoke();
        }

        public static void Stop()
        {
            OnGameStop.Invoke();
        }

        public static void Start()
        {
            OnGameStart.Invoke();
        }
    }
}