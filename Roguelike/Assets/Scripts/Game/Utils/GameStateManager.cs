using Game.Event;
using UnityEngine;

namespace Game.Utils
{
    public class GameStateManager : MonoBehaviour
    {
        public static bool IsStopped { get; private set; }

        private void Awake()
        {
            GlobalEventManager.OnGameStart.AddListener(() => IsStopped = false);
            GlobalEventManager.OnGameStop.AddListener(() => IsStopped = true);
        }
    }
}