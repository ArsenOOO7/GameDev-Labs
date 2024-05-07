using System;
using Game.Event;
using Game.Level;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject losePanel;
        [SerializeField] private float startHealth;
        private float _health;

        private void Awake()
        {
            GlobalEventManager.OnGameStart.AddListener(() =>
            {
                _health = startHealth;
                EntityEventManager.ChangeHp(_health);
                losePanel.SetActive(false);
            });
            GlobalEventManager.OnGameStop.AddListener(() =>
            {
                losePanel.SetActive(true);
            });
        }

        private void Start()
        {
            _health = startHealth;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            EntityEventManager.ChangeHp(_health);
            if (_health <= 0)
            {
                GlobalEventManager.Stop();
            }
        }
        
        
    }
}