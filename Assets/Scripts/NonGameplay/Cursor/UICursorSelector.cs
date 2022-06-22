using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class UICursorSelector : MonoBehaviour
{
    private EventQueue eventQueue;

    private bool hasRectTransform;
    private RectTransform rectTransform;
    private BoxCollider2D thisCollider = null;
    private Button lastSelectedButton = null;

    private UIInputPositionController inputController;
    private UISelectHandler selectHandler;
    private SpriteRenderer spriteRenderer;
    private ResizeBoxCollider2D collResizer;

    private MultiplayerEventSystem eventSystem;

    private Vector2 inputVector;

    void Start()
    {
        eventQueue = FindObjectOfType<EventQueue>();

        #region TRYGETCOMPONENTS
        if (!TryGetComponent<SpriteRenderer>(out spriteRenderer))
            throw new MissingComponentException("Cursor prefab is missing a SpriteRenderer component! Add one for visuals to work!");

        if (!TryGetComponent<UIInputPositionController>(out inputController))
            throw new MissingComponentException("Cursor is missing a UIInputPositionController-type script!");
        if (!TryGetComponent<UISelectHandler>(out selectHandler))
            throw new MissingComponentException("Cursor is missing a UISelectHandler-type script!");

        if (!TryGetComponent<BoxCollider2D>(out thisCollider))
            throw new MissingComponentException("Cursor is missing a BoxCollider2D Component! Will not be able to interact with buttons!");
        if (!TryGetComponent<ResizeBoxCollider2D>(out collResizer))
            throw new MissingComponentException("Cursor is missing a ResizeBoxCollider2D Component! Will not be able to interact with buttons!");

        if (!TryGetComponent<MultiplayerEventSystem>(out eventSystem))
            throw new MissingComponentException("Cursor is missing a MultiplayerEventSystem component, you will not see highlights from it!");
        hasRectTransform = TryGetComponent<RectTransform>(out rectTransform);
        #endregion

        eventQueue.Subscribe(EventType.PLAYER_REGISTERED, onPlayerRegistered);
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.PLAYER_REGISTERED, onPlayerRegistered);
    }

    #region ON INPUT EVENTS
    private void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    /* For future generations:
     * Using Unity's InputSystem, this gets sent both on button PRESS (DOWN) and button RELEASE (UP).
     * Dunno if it is documented properly.
     * One or the other can be filtered via value.isPressed, as seen immediately below.
     */
    private void OnClick(InputValue value)
    {
        if (value.isPressed == false)
        {
            //Debug.LogWarning("On Click button release");
            return;
        }

        //Debug.LogWarning("On Click button press");
        // simply force Invoke whatever is on the onClick and inform about the click. any special effects should be handled by the button itself.
        if (lastSelectedButton != null)
        {
            lastSelectedButton.onClick.Invoke();
            eventQueue.AddEvent(new ButtonPressedEventData(this, lastSelectedButton));
        }
    }
    #endregion

    private void onPlayerRegistered(EventData eventData)
    {
        PlayerRegisteredEventData data = (PlayerRegisteredEventData)eventData;

        if (data.player.cursorObject == this)
        {
            assignCursorSprite(data.player.playerIndex);
        }
        collResizer.Resize();
    }

    private void assignCursorSprite(int fromIndex)
    {
        spriteRenderer.sprite = PlayerManager.Instance.GetCursorSpriteAtIndex(fromIndex);
        // ~~if sprite is null after this, expect a fun stack trace traversal
    }

    void Update()
    {
        if (hasRectTransform) inputController.MoveRectTransform(rectTransform, inputVector);
        else inputController.MoveTransform(this.transform, inputVector);

        Collider2D firstOverlappedCollider = selectHandler.CheckForColliderOverlap(thisCollider);
        if (firstOverlappedCollider != null) selectHandler.OnColliderOverlap(firstOverlappedCollider, out lastSelectedButton);
        else lastSelectedButton = null;

        /** following block requires EventSystem. not really to process anything (certainly DO NOT use UI Input Modules),
          * but for... the highlighting.
          * AND it still only allows for 1 highlight per canvas (1 selection per cursor object at least, phew.)
          */
        if (lastSelectedButton != null) eventSystem.SetSelectedGameObject(lastSelectedButton.gameObject);
        else eventSystem.SetSelectedGameObject(null);

        //print(eventSystem.currentSelectedGameObject);
    }
}
