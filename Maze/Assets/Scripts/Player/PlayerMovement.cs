using System.Collections;
using UnityEngine;

namespace Player
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

        [Header("GroundCheck"), SerializeField]
        private LayerMask isGround;

        private float _playerHeight;
        private float _horizontalInput;
        private float _verticalInput;

        private Vector3 _moveDirection;
        private Rigidbody _rigidbody;

        private bool _readyToJump;
        private bool _grounded;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;

            _readyToJump = true;
            _playerHeight = transform.localScale.y;
        }

        private void Update()
        {
            float maxDistance = _playerHeight + 0.01f;
            _grounded = Physics.Raycast(transform.position, Vector3.down, maxDistance, isGround);

            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");

            Jump();
            SpeedControl();

            _rigidbody.drag = _grounded ? groundDrag : 0;
        }

        private void FixedUpdate()
        {
            _moveDirection = (orientation.forward * _verticalInput) + (orientation.right * _horizontalInput);

            var forceVector = _moveDirection.normalized * (10f * moveSpeed);
            forceVector *= _grounded ? 1 : airMultiplier;
            _rigidbody.AddForce(forceVector, ForceMode.Force);

            /*if (_grounded)
            {
                _rigidbody.AddForce(_moveDirection.normalized * (10f * moveSpeed), ForceMode.Force);
            }
            else if (!_grounded)
            {
                _rigidbody.AddForce(_moveDirection.normalized * (10f * moveSpeed * airMultiplier), ForceMode.Force);
            }*/
        }

        private void SpeedControl()
        {
            var oldVelocity = _rigidbody.velocity;
            var flatVelocity = new Vector3(oldVelocity.x, 0f, oldVelocity.z);
            if (flatVelocity.magnitude > moveSpeed)
            {
                var limitedVelocity = flatVelocity.normalized * moveSpeed;
                _rigidbody.velocity = new Vector3(limitedVelocity.x, _rigidbody.velocity.y, limitedVelocity.z);
            }
        }

        private void Jump()
        {
            if (!Input.GetKey(jumpKey) || !_readyToJump || !_grounded)
            {
                return;
            }

            _readyToJump = false;
            var oldVelocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3(oldVelocity.x, 0f, oldVelocity.z);
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(ResetJump());
        }

        private IEnumerator ResetJump()
        {
            yield return new WaitForSeconds(jumpCooldown);
            _readyToJump = true;
        }
    }
}