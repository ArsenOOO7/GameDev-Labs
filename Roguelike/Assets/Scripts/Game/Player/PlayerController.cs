using System;
using Game.Event;
using Game.Level;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float startHealth;
        private float _health;

        private void Start()
        {
            _health = startHealth;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                GlobalEventManager.Stop();
            }
        }
        
        
    }
}