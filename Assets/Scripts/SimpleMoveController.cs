using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

// Based on ThirdPersonController. Until it is made in good code, RequireComponent the base parts.
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(StarterAssetsInputs))]
public class SimpleMoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
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

    //private PlayerInput _playerInput;     // only for IsCurrentDeviceMouse

    private CharacterController _controller;
    private StarterAssetsInputs _input;       // MonoBehaviour?? (custom class for inputs)
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;  // cache main camera
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<StarterAssetsInputs>();
        //_playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
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
        move();
    }

    /**/
    private void move()
    {
        float targetSpeed = moveSpeed;

        // if there is no input, set the target speed  to 0
        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;

        // ~~check whether _input.move.magnitude or 1f controls better and why (analogMovement doesn't seem to do much otherwise)
        //float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;
        float inputMagnitude = _input.move.magnitude;

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
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // if there is a move input rotate player when the player is moving
        if (_input.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime)
                        + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    /**/
}
