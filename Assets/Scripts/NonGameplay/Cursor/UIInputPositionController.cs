using UnityEngine;

public class UIInputPositionController : MonoBehaviour
{
    [SerializeField] private float pixelsPerSecond;

    public void MoveTransform(Transform transform, Vector2 byInputVector)
    {
        transform.Translate(new Vector3(byInputVector.x, byInputVector.y, 0) * Time.deltaTime * pixelsPerSecond);
    }

    public void MoveRectTransform(RectTransform rectTransform, Vector2 byInputVector)
    {
        rectTransform.anchoredPosition += byInputVector * Time.deltaTime * pixelsPerSecond;
    }
}
