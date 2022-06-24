using UnityEngine;

// SmoothDampAngle for the rotation based on ThirdPersonController.
[RequireComponent(typeof(Rigidbody))]
public class SimpleMoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    [Header("Fine-tune settings")]
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    [SerializeField] private float rotationSmoothTime = 0.12f;

    private float _targetRotation = 0.0f;
    private float _rotationVelocity;

    private Rigidbody _rigidbody;

    private Camera _mainCamera;     // used to make movements relative to the camera

    //Data Events
    public float speedX = 1;

    private void Awake()
    {
        _mainCamera = Camera.main;

        _rigidbody = GetComponent<Rigidbody>();
    }

    public float Move(Vector2 input)
    {
        Vector3 inputDirection = new Vector3(input.x, 0.0f, input.y);

        if (input != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            Vector3 finalMovement = inputDirection.magnitude * targetDirection * moveSpeed * speedX;
            _rigidbody.velocity = finalMovement;
            //MovePosition(_rigidbody.position + targetDirection * (moveSpeed * Time.deltaTime));

            return finalMovement.magnitude;
        } 
        else
        {
            StopVelocity();
            return 0f;
        }
    }

    public void StopVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
