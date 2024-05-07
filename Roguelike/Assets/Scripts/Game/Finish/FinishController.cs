using System;
using UnityEngine;

namespace Game.Finish
{
    public class FinishController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
            }
        }
    }
}