using Game.Event;
using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(EnemyAI))]
    public class EnemyController : MonoBehaviour
    {
        public static int AllEnemies = 0;

        [SerializeField] private float startHealth;
        private float _health;

        private EnemyAI _enemyAi;
        private bool _killed = false;

        private void Awake()
        {
            _enemyAi = GetComponent<EnemyAI>();
            GlobalEventManager.OnGameStart.AddListener(() =>
            {
                _health = startHealth;
                _enemyAi.AllowMoving(true);
                EntityEventManager.ChangeEnemyCount(++AllEnemies);
                _killed = false;
            });
        }

        private void Start()
        {
            _health = startHealth;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0 && !_killed)
            {
                _killed = true;
                transform.position = new Vector3(999, 999, 0);
                _enemyAi.AllowMoving(false);
                EntityEventManager.ChangeEnemyCount(--AllEnemies);
            }
        }

        public static bool AllEnemiesDestroyed()
        {
            return AllEnemies == 0;
        }
    }
}