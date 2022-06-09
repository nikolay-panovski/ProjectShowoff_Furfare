using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class UICursorSelector : MonoBehaviour
{
    private bool hasRectTransform;
    private RectTransform rectTransform;
    private BoxCollider2D thisCollider = null;
    private Button lastSelectedButton = null;

    private UIInputPositionController inputController;
    private UISelectHandler selectHandler;

    private Vector2 inputVector;

    private PlayerConfig attachedPlayer;

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
        // TODO: DECOUPLE!!
        if (lastSelectedButton.TryGetComponent<ButtonClickHandle>(out ButtonClickHandle handle))
        {
            // select and attach character model on first click
            if (attachedPlayer.characterModel == null)
            {
                attachedPlayer.characterModel = handle.GetCharacterModel();
                attachedPlayer.isReady = true;
            }
            // BUG: would de-select/de-attach on second click, but the Click event gets fired twice at a time
            //else attachedPlayer.characterModel = null;
            //attachedPlayer.isReady = false;
        }
        
    }
    #endregion

    void Update()
    {
        if (hasRectTransform) inputController.MoveRectTransform(rectTransform, inputVector);
        else inputController.MoveTransform(this.transform, inputVector);

        Collider2D firstOverlappedCollider = selectHandler.CheckForColliderOverlap(thisCollider);
        if (firstOverlappedCollider != null) selectHandler.OnColliderOverlap(firstOverlappedCollider, out lastSelectedButton);
        else lastSelectedButton = null;

        // following block requires EventSystem. not really to process anything (certainly DO NOT use UI Input Modules),
        // but for... the highlighting.
        // AND it still only allows for 1 highlight per canvas (1 selection per cursor object at least, phew.)
        if (lastSelectedButton != null) GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(lastSelectedButton.gameObject);
        else GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(null);

        //print(GetComponent<MultiplayerEventSystem>().currentSelectedGameObject);
    }

    public void SetAttachedPlayer(PlayerConfig player)
    {
        attachedPlayer = player;
    }
}
