using System;
using Event;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class PlayerController : MonoBehaviour
    {

        private short _crashes = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                ++_crashes;
                RoadEventManager.PlayerCrash(_crashes);
                if (_crashes >= 5)
                {
                    GlobalEventManager.GameStop();
                }
            }
        }
    }
}