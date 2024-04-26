using UnityEngine.Events;

namespace Event
{
    public static class RoadEventManager
    {
        public static readonly UnityEvent<float> OnPlayerSpeedChanges = new();

        public static void ChangePlayerSpeed(float speed)
        {
            OnPlayerSpeedChanges.Invoke(speed);
        }
    }
}