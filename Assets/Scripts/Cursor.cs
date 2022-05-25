using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    private void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        transform.Translate(inputVector.x, inputVector.y, 0);
    }
}
