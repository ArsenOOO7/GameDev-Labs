using Event;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        private float _currentSpeed;

        [SerializeField] private float maxSpeed, minSpeed;
        [SerializeField] private float moveSpeed;

        private float _horizontalInput, _verticalInput;
        private Vector3 _moveDirection;

        private bool _gameFinished = false;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _currentSpeed = moveSpeed;
            RoadEventManager.ChangePlayerSpeed(_currentSpeed);
            GlobalEventManager.OnGameStop.AddListener(() =>
            {
                _gameFinished = true;
            });
        }

        private void Update()
        {
            if (_gameFinished)
            {
                return;
            }
            
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");
            _currentSpeed += _verticalInput * Time.deltaTime * moveSpeed;
            _currentSpeed = Mathf.Clamp(_currentSpeed, minSpeed, maxSpeed);
            RoadEventManager.ChangePlayerSpeed(_currentSpeed);

            _moveDirection = Vector3.right * _horizontalInput;
            _rigidBody.AddForce(_moveDirection.normalized * moveSpeed, ForceMode.Force);
            SpeedControl();
        }

        private void SpeedControl()
        {
            if (_gameFinished)
            {
                return;
            }
            
            Vector3 oldVelocity = _rigidBody.velocity;
            Vector3 flatVelocity = new Vector3(oldVelocity.x, 0, oldVelocity.z);

            if (flatVelocity.magnitude > _currentSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * _currentSpeed;
                _rigidBody.velocity = new Vector3(limitedVelocity.x, _rigidBody.velocity.y, limitedVelocity.z);
            }

            Quaternion oldRotation = _rigidBody.rotation;
            _rigidBody.rotation = new Quaternion(oldRotation.x, oldRotation.y, 0f, oldRotation.w);
        }
    }
}