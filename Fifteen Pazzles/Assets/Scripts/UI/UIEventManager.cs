using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.UI
{
    public class UIEventManager : MonoBehaviour
    {
        public static readonly UnityEvent<int> ON_MOVEMENENT = new();

        public static void OnNewMove(int moves)
        {
            ON_MOVEMENENT.Invoke(moves);
        }
    }
}