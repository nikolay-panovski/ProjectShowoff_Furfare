using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UICursorSelector : MonoBehaviour
{
    private Vector2 inputVector;

    private bool hasRectTransform;
    private RectTransform rectTransform;

    private BoxCollider2D thisCollider = null;

    private Button lastSelectedButton = null;


    private UIInputPositionController inputController;
    private UISelectHandler selectHandler;

    // Start is called before the first frame update
    void Start()
    {
        hasRectTransform = TryGetComponent<RectTransform>(out rectTransform);
        if (!TryGetComponent<UIInputPositionController>(out inputController))
            throw new MissingComponentException("Cursor is missing a UIInputPositionController-type script!");
        if (!TryGetComponent<UISelectHandler>(out selectHandler))
            throw new MissingComponentException("Cursor is missing a UISelectHandler-type script!");

        if (!TryGetComponent<BoxCollider2D>(out thisCollider))
            throw new MissingComponentException("Cursor is missing a BoxCollider2D Component! Will not be able to interact with buttons!");
    }

    #region ON INPUT EVENTS
    private void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    private void OnClick(InputValue value)
    {
        if (lastSelectedButton != null) lastSelectedButton.onClick.Invoke();
    }
    #endregion

    void Update()
    {
        if (hasRectTransform) inputController.MoveRectTransform(rectTransform, inputVector);
        else inputController.MoveTransform(this.transform, inputVector);

        Collider2D firstOverlappedCollider = selectHandler.CheckForColliderOverlap(thisCollider);
        if (firstOverlappedCollider != null) selectHandler.OnColliderOverlap(firstOverlappedCollider, out lastSelectedButton);
    }

}
