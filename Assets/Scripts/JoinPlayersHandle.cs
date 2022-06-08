using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class JoinPlayersHandle : MonoBehaviour
{
    private EventQueue eventQueue;

    private void Start()
    {
        eventQueue = FindObjectOfType<EventQueue>();
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("Player joined");

        // child this InputObject to the canvas; its Cursor should be a child of this
        player.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        // TODO: Resolve hierarchy so this can indeed be DontDestroyOnLoad-ed
        DontDestroyOnLoad(player);

        eventQueue.AddEvent(new ControllerJoinedEventData(player));
    }
}
