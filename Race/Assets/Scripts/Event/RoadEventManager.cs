using UnityEngine.Events;

namespace Event
{
    public static class RoadEventManager
    {
        public static readonly UnityEvent<float> OnPlayerSpeedChanges = new();
        public static readonly UnityEvent<float> OnPlayerCrash = new();

        public static void ChangePlayerSpeed(float speed)
        {
            OnPlayerSpeedChanges.Invoke(speed);
        }
        
        public static void PlayerCrash(float crashes)
        {
            OnPlayerCrash.Invoke(crashes);
        }
    }
}