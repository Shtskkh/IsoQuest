using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public enum PlayerState
    {
        Idle, Running
    }
    
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform playerModel;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Animator animator;
    
        private InputAction _move;
        private Vector3 _moveInput;
        private PlayerState _playerState;

        private void Awake()
        {
            _move = InputSystem.actions.FindAction("Movement");
            _playerState = PlayerState.Idle;
        }

        private void Update()
        {
            HandleInput();
            Look();

            switch (_playerState)
            {
                case PlayerState.Idle:
                    if (_moveInput != Vector3.zero)
                    {
                        animator.CrossFadeInFixedTime("Running", 0.2f);
                        _playerState = PlayerState.Running;
                    }

                    break;
                case PlayerState.Running:
                    if (_moveInput == Vector3.zero)
                    {
                        animator.CrossFadeInFixedTime("Idle", 0.2f);
                        _playerState = PlayerState.Idle;
                    }
                    break;
            }
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

