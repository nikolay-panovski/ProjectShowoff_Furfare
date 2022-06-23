using UnityEngine;
using UnityEngine.UI;

public class GameStartButtonHandle : MonoBehaviour
{
    private EventQueue eventQueue;

    private Image displayImage;
    private Button functionalButton;
    private BoxCollider2D buttonCollider;

    private void Awake()
    {
        eventQueue = FindObjectOfType<EventQueue>();

        if (!TryGetComponent<Image>(out displayImage))
            throw new MissingComponentException("Button is missing an Image component! It will not display visually!");
        if (!TryGetComponent<Button>(out functionalButton))
            throw new MissingComponentException("Button is missing a functional Button component! It will not respond on press!");
        if (!TryGetComponent<BoxCollider2D>(out buttonCollider))
            throw new MissingComponentException("Button is missing a BoxCollider2D component! It will not respond on press!");

        //eventQueue.Subscribe(EventType.BUTTON_PRESSED, onButtonPressed);
    }

    void OnDestroy()
    {
        //eventQueue.Unsubscribe(EventType.BUTTON_PRESSED, onButtonPressed);
    }

    private void Update()
    {
        enableOnPlayersReady();
    }

    private void onButtonPressed(EventData eventData)
    {
        //enableOnPlayersReady();   // unfortunately wrong event - isReady gets set on character selected, which is an event raised
                                    // *in response to* button pressed
    }

    private void enableOnPlayersReady()
    {
        if (PlayerManager.Instance.GetAllPlayersReady())
        {
            displayImage.enabled = true;
            functionalButton.enabled = true;
            buttonCollider.enabled = true;
        }
        else
        {
            displayImage.enabled = false;
            functionalButton.enabled = false;
            buttonCollider.enabled = false;
        }
    }
}
