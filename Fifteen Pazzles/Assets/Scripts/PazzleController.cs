using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PazzleController : MonoBehaviour
    {
        private General _generalController;

        private void Start()
        {
            _generalController = gameObject.GetComponentInParent<General>();
            Console.WriteLine(11);
        }

        public void OnMouseDownClick()
        {
            _generalController.OnMouseDownClick(gameObject);
        }
    }
}