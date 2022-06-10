using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandle : MonoBehaviour
{
    private EventQueue eventQueue;

    [SerializeField] private GameObject characterModel;

    private void Start()
    {
        eventQueue = FindObjectOfType<EventQueue>();
    }

    private void OnDestroy()
    {
        
    }

    public void OnCharacterButtonClick()
    {
        // the one who clicked the button should be assigned the character
        //eventQueue.AddEvent(new CharacterSelectedEventData(characterModel));
    }

    public GameObject GetCharacterModel()
    {
        return characterModel;
    }
}
