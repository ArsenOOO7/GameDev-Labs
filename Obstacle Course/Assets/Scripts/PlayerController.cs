using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rigidBody;

        private bool _finished = false;
        private bool _trulyFinished = false;
        private float _playerWaitingTime;
        private float _totalGameTime = 0f;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Finish"))
            {
                _finished = true;
            }

            if (collider.CompareTag("Start"))
            {
                if (_finished)
                {
                    GlobalEventManager.PlayerFinished(_totalGameTime);
                    _trulyFinished = true;
                }
            }
        }

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }


        private void Update()
        {
            if (_trulyFinished)
            {
                return;
            }

            if (_playerWaitingTime > 5)
            {
                GlobalEventManager.PlayerDeath();
                return;
            }

            _playerWaitingTime = _rigidBody.velocity.magnitude == 0 ? _playerWaitingTime + Time.deltaTime : 0;
            _totalGameTime += Time.deltaTime;
            GlobalEventManager.UpdateWaitingTime(_playerWaitingTime);
        }
    }
}