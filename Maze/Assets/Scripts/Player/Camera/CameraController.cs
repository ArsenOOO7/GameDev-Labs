using System;
using DefaultNamespace;
using UnityEngine;

namespace Player.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float sensivityX = 800f;
        [SerializeField] private float sensivityY = 800f;

        [SerializeField] private Transform orientation;

        private float _rotateX;
        private float _rotateY;

        private bool _gameStopped = false;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GlobalEventManager.OnGameFinish.AddListener(() => { _gameStopped = true; });
        }

        private void Update()
        {
            if (_gameStopped)
            {
                return;
            }

            _rotateY += Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensivityX;
            _rotateX -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensivityY;

            _rotateX = Mathf.Clamp(_rotateX, -90f, 90f);
            transform.rotation = Quaternion.Euler(_rotateX, _rotateY, 0);
            orientation.rotation = Quaternion.Euler(0, _rotateY, 0);
        }
    }
}