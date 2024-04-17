using UnityEngine;

namespace FPCamera
{
    public class FirstPersonCameraHolder : MonoBehaviour
    {
        [SerializeField] private Transform cameraPosition;

        private void Update()
        {
            transform.position = cameraPosition.position;
        }
    }
}