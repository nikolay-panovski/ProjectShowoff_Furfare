using UnityEngine;
using UnityEngine.InputSystem;

// Based on ThirdPersonController.
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class SimpleMoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector2 moveInput;  // store OnMove results here
    
    [Header("Fine-tune settings")]
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    [SerializeField] private float rotationSmoothTime = 0.12f;

    //[Tooltip("Acceleration and deceleration")]
    //[SerializeField] private float speedChangeRate = 10.0f;

    //private float _speed;   // not needed with direct move with moveSpeed, preserving in case something messes up
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;

    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    private Camera _mainCamera;     // used to make movements relative to the camera

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        move(moveInput);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void move(Vector2 input)
    {
        Vector3 inputDirection = new Vector3(input.x, 0.0f, input.y);

        /**
        if (input == Vector2.zero)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        /**/

        if (input != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            _rigidbody.velocity = inputDirection.magnitude * targetDirection * moveSpeed;
            //MovePosition(_rigidbody.position + targetDirection * (moveSpeed * Time.deltaTime));
        } else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}