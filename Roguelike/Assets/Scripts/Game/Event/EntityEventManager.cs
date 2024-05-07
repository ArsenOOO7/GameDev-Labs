using UnityEngine.Events;

namespace Game.Event
{
    public static class EntityEventManager
    {
        public static readonly UnityEvent<float> OnHpChange = new();
        public static readonly UnityEvent<int> OnChestOpen = new();
        public static readonly UnityEvent<int> OnEnemyCountChange = new();

        public static void ChangeHp(float health)
        {
            OnHpChange.Invoke(health);
        }

        public static void OpenChest(int openedTotal)
        {
            OnChestOpen.Invoke(openedTotal);
        }

        public static void ChangeEnemyCount(int enemies)
        {
            OnEnemyCountChange.Invoke(enemies);
        }
    }
}