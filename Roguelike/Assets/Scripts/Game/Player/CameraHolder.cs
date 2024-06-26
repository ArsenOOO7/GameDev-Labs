﻿using UnityEngine;

namespace Game.Player
{
    public class CameraHolder : MonoBehaviour
    {
        [SerializeField] private string cameraBoundsObjectName;
        [SerializeField] private GameObject playerCharacter;
        [SerializeField] private Transform player;
        [SerializeField] private float lerpSpeed = 1f;

        private Camera _camera;
        private BoxCollider2D _boundsCollider;
        private Vector3 _offset;
        private Bounds _bounds;


        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _offset = transform.position - player.position;
            _boundsCollider = GameObject.Find("CameraBounds").GetComponent<BoxCollider2D>();

            InitBounds(_boundsCollider);
        }

        private void InitBounds(BoxCollider2D collider)
        {
            var bounds = collider.bounds;

            var height = _camera.orthographicSize;
            var width = height * _camera.aspect;

            var minX = bounds.min.x + width;
            var maxX = bounds.extents.x - width;

            var minY = bounds.min.y + height;
            var maxY = bounds.extents.y - height;

            _bounds = new Bounds();
            _bounds.SetMinMax(
                new Vector3(minX, minY, 0f),
                new Vector3(maxX, maxY, 0f)
            );
        }

        private void Update()
        {
            Vector3 lerpPos = Vector3.Lerp(_camera.transform.position, player.position + _offset,
                Time.deltaTime * lerpSpeed);
            _camera.transform.position = GetCameraBounds(lerpPos);
        }

        private Vector3 GetCameraBounds(Vector3 targetPosition)
        {
            return new Vector3(
                Mathf.Clamp(targetPosition.x, _bounds.min.x, _bounds.max.x),
                Mathf.Clamp(targetPosition.y, _bounds.min.y, _bounds.max.y),
                _camera.transform.position.z
            );
        }
    }
}