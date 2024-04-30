using System;
using DefaultNamespace.Enemy;
using UnityEngine.Events;

namespace Event
{
    public static class EnemyEventManager
    {
        public static readonly UnityEvent<EnemyController> OnEnemyOutOufBounds = new();


        public static void EnemyOutOfBounds(EnemyController controller)
        {
            OnEnemyOutOufBounds.Invoke(controller);
        }
    }
}