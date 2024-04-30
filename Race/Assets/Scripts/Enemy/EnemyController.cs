using System;
using System.Collections;
using Event;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private Coroutine _coroutine;
        private float _speed, _currentSpeed;
        private bool _onRoad;

        [SerializeField] private Transform endPosition;

        private void Start()
        {
            RoadEventManager.OnPlayerSpeedChanges.AddListener(playerSpeed =>
            {
                if (_onRoad)
                {
                    _currentSpeed = _speed - playerSpeed;
                }
            });
        }

        private void ManageCoroutine(bool start)
        {
            if (start)
            {
                _coroutine ??= StartCoroutine(Move());
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

        private IEnumerator Move()
        {
            while (_onRoad)
            {
                if (endPosition.position.z > transform.position.z)
                {
                    EnemyEventManager.EnemyOutOfBounds(this);
                }
                else
                {
                    transform.position += Vector3.forward * (Time.deltaTime * _currentSpeed);
                }

                yield return null;
            }
        }

        public void SpawnOnRoad(Transform newTransform, float newSpeed)
        {
            var newPositon = newTransform.position;
            transform.position = new Vector3(newPositon.x + Random.Range(-0.5f, 0.5f), transform.position.y, newPositon.z);
            transform.rotation = newTransform.rotation;
            gameObject.SetActive(true);
            _speed = newSpeed;
            _currentSpeed = _speed;
            _onRoad = true;
            ManageCoroutine(true);
        }

        public void RemoveFromRoad()
        {
            ManageCoroutine(false);
            _onRoad = false;
            gameObject.SetActive(false);
        }
    }
}