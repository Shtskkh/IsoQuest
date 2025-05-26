using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform playerModel;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float moveSpeed;
    
        private InputAction _move;
        private Vector3 _moveInput;

        private void Awake()
        {
            _move = InputSystem.actions.FindAction("Movement");
        }

        private void Update()
        {
            HandleInput();
            Look();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void HandleInput()
        {
            var moveInput = _move.ReadValue<Vector2>();
            _moveInput = new Vector3(moveInput.x, 0, moveInput.y);
        }

        private void Look()
        {
            if (_moveInput == Vector3.zero) return;

            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            
            var skwedInput = matrix.MultiplyPoint3x4(_moveInput);
            
            var relative = transform.position + skwedInput - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = rotation;
        }

        private void Move()
        {
            rb.MovePosition(transform.position + transform.forward * (_moveInput.magnitude * moveSpeed * Time.deltaTime));
        }
    }
}

