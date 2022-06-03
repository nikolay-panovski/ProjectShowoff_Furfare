using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputPositionController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 inputVector;

    private void Update()
    {
        move(inputVector);
    }

    private void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    private void OnSubmit(InputValue value)
    {
        Debug.Log("clicked");
    }

    private void move(Vector2 inputVector)
    {
        transform.Translate(new Vector3(inputVector.x, inputVector.y, 0) * Time.deltaTime * speed);
    }
}
