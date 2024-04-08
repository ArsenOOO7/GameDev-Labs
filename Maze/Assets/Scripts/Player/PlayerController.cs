using System;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private float _points = 0f;
        private short _collectedTargets = 0;


        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag.Equals("Target"))
            {
                Destroy(collider.gameObject);
                ++_collectedTargets;
                _points += 200;
                UIEventManager.ChangeScore(_points);
            }
        }
    }
}