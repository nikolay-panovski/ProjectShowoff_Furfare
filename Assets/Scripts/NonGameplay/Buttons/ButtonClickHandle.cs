using UnityEngine;
using UnityEngine.UI;

/** DISCLAIMER: performEventButtonAction() is currently specific to CharacterSelect buttons,
  * whose work is to pass information about the character model they are associated with.
  * Subclass that part out whenever other buttons need to send events, but with different information.
  */
public class ButtonClickHandle : MonoBehaviour
{
    private EventQueue eventQueue;

    private Button button;

    [SerializeField] private GameObject characterModel;
    [SerializeField] private int characterID;   // probably "dirty but lazy". Make sure there is a match in the numbers between model (filename) and ID.

    private void Start()
    {
        button = GetComponent<Button>();

        eventQueue = FindObjectOfType<EventQueue>();

        eventQueue.Subscribe(EventType.BUTTON_PRESSED, onButtonPressed);
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.BUTTON_PRESSED, onButtonPressed);
    }

    private void onButtonPressed(EventData eventData)
    {
        ButtonPressedEventData data = (ButtonPressedEventData)eventData;
        // check if specifically this button is pressed
        if (data.pressedButton == button)
        {
            performEventButtonAction(data.byCursor);
        }
    }

    private void performEventButtonAction(UICursorSelector byCursor)
    {
        // SPECIFIC: character selected
        // characterID is - 1 for the human-readable form of player1 = ID "1" etc. vs a 0-indexed array
        eventQueue.AddEvent(new CharacterSelectedEventData(byCursor, characterModel, characterID - 1));
    }
}
