using System;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class PlayerCameraHolder : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float lerpSpeed = 2f;

        private Camera _mainCamera;
        private Vector3 _offset;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            _offset = transform.position - player.position;
        }

        private void Update()
        {
            transform.position =
                Vector3.Lerp(transform.position, player.position + _offset, lerpSpeed * Time.deltaTime);
        }
    }
}