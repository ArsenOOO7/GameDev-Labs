using System;
using UnityEngine;

namespace Rotator
{
    public class RotatorController : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(new Vector3(90, 180, 90) * Time.deltaTime);       
        }
    }
}