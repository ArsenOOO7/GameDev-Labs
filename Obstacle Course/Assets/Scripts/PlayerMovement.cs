using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement"), SerializeField] private float moveSpeed;
        [SerializeField] private float groundDrag;
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCooldown;
        [SerializeField] private float airMultiplier;
        [SerializeField] private Transform orientation;

        [Header("KeyBinds"), SerializeField] private KeyCode jumpKey = KeyCode.Space;

        private float _playerHeight;
        private float _horizontalInput;
        private float _verticalInput;

        private Vector3 _moveDirection;
        private Rigidbody _rigidBody;

        private bool _readyToJump;
        private bool _grounded;

        private bool _gameStopped = false;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.freezeRotation = true;

            _readyToJump = true;
            _playerHeight = transform.localScale.y;
            
            GlobalEventManager.OnPlayerFinished.AddListener(() => _gameStopped = true);
            GlobalEventManager.OnPlayerDeath.AddListener(() => _gameStopped = true);
        }

        private void Update()
        {
            if (_gameStopped)
            {
                return;
            }

            float maxDistance = _playerHeight + 0.1f;
            _grounded = Physics.Raycast(transform.position, Vector3.down, maxDistance);

            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");

            Jump();
            SpeedControl();

            _rigidBody.drag = _grounded ? groundDrag : 0;
        }

        private void FixedUpdate()
        {
            if (_gameStopped)
            {
                return;
            }

            _moveDirection = (orientation.forward * _verticalInput) + (orientation.right * _horizontalInput);

            var forceVector = _moveDirection.normalized * (10f * moveSpeed);
            forceVector *= _grounded ? 1 : airMultiplier;
            _rigidBody.AddForce(forceVector, ForceMode.Force);
        }

        private void SpeedControl()
        {
            var oldVelocity = _rigidBody.velocity;
            var flatVelocity = new Vector3(oldVelocity.x, 0f, oldVelocity.z);
            if (flatVelocity.magnitude > moveSpeed)
            {
                var limitedVelocity = flatVelocity.normalized * moveSpeed;
                _rigidBody.velocity = new Vector3(limitedVelocity.x, _rigidBody.velocity.y, limitedVelocity.z);
            }
        }

        private void Jump()
        {
            if (!Input.GetKey(jumpKey) || !_readyToJump || !_grounded)
            {
                return;
            }

            _readyToJump = false;
            var oldVelocity = _rigidBody.velocity;
            _rigidBody.velocity = new Vector3(oldVelocity.x, 0f, oldVelocity.z);
            _rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(ResetJump());
        }

        private IEnumerator ResetJump()
        {
            yield return new WaitForSeconds(jumpCooldown);
            _readyToJump = true;
        }
    }
}