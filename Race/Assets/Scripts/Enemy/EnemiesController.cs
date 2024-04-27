using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Enemy
{
    public class EnemiesController : MonoBehaviour
    {
        private Coroutine _coroutine;

        [SerializeField] private Transform right;
        [SerializeField] private Transform left;

        [SerializeField] private List<EnemyController> pool;
        [SerializeField] private int maxEnemies = 5;


        private readonly List<EnemyController> _onRoad = new();

        private void Start()
        {
            foreach (var enemyController in pool)
            {
                enemyController.RemoveFromRoad();
            }

            EnemyEventManager.OnEnemyOutOufBounds.AddListener(controller =>
            {
                _onRoad.Remove(controller);
                controller.RemoveFromRoad();
                pool.Add(controller);
            });
            ManageCoroutine(true);
        }

        private void ManageCoroutine(bool start)
        {
            if (start)
            {
                _coroutine ??= StartCoroutine(SpawnEnemies());
            }
            else
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                    _coroutine = null;
                }
            }
        }

        private void RemoveAllFromRoad()
        {
            foreach (EnemyController controller in _onRoad)
            {
                controller.RemoveFromRoad();
            }

            pool.AddRange(_onRoad);
            _onRoad.Clear();
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                if (_onRoad.Count <= maxEnemies && pool.Count != 0 && Random.Range(0, 4) > 1)
                {
                    var enemy = pool[Random.Range(0, pool.Count)];

                    if (Random.Range(0, 2) == 1)
                    {
                        enemy.SpawnOnRoad(right, Random.Range(5, 15));
                    }
                    else
                    {
                        enemy.SpawnOnRoad(left, Random.Range(-5, -10));
                    }

                    _onRoad.Add(enemy);
                    pool.Remove(enemy);
                }

                yield return new WaitForSeconds(1);
            }
        }
    }
}