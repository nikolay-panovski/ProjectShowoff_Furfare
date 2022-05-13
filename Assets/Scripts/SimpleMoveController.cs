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

    [Tooltip("Acceleration and deceleration")]
    [SerializeField] private float speedChangeRate = 10.0f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    [SerializeField] private float gravity = -15.0f;

    private float _speed;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;  // cache main camera
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        // necessary actions
        // Move, Fire
        // later on: Grab (poll input for grab projectile)

        //JumpAndGravity();
        // ---- "gravity" part of JumpAndGravity() (linear)
        if (_verticalVelocity < 0.0f)
        {
            _verticalVelocity = -2f;
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
        // ----
        //GroundedCheck();
        move(moveInput);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    /**/
    private void move(Vector2 input)
    {
        Vector3 inputDirection = new Vector3(input.x, 0.0f, input.y).normalized;

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

            //if (_rigidbody.velocity.magnitude < moveSpeed)
            {
                _rigidbody.MovePosition(_rigidbody.position + targetDirection * (moveSpeed * Time.deltaTime));
            }
        }

        
    }
    /**/

    /**
    private void move(Vector2 input)
    {
        float targetSpeed = moveSpeed;

        // if there is no input, set the target speed  to 0
        if (input == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_rigidbody.velocity.x, 0.0f, _rigidbody.velocity.z).magnitude;

        float speedOffset = 0.1f;

        float inputMagnitude = input.magnitude;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * speedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        // normalise input direction
        Vector3 inputDirection = new Vector3(input.x, 0.0f, input.y).normalized;

        // if there is a move input rotate player when the player is moving
        if (input != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player (Unity Rigidbody physics version)
        _rigidbody.AddForce(targetDirection.normalized * (_speed * Time.deltaTime)
                          + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime,
                          ForceMode.VelocityChange);
    }
    /**/
}
