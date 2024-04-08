using UnityEngine;

namespace Player.Camera
{
    public class CameraHolder : MonoBehaviour
    {
        [SerializeField] private Transform cameraPosition;

        private void Update()
        {
            transform.position = cameraPosition.position;
        }
    }
}