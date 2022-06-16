using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class JoinPlayersHandle : MonoBehaviour
{
    private EventQueue eventQueue;

    private void Awake()
    {
        eventQueue = FindObjectOfType<EventQueue>();

        DontDestroyOnLoad(gameObject);  // Player Input Manager gameobject
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        //Debug.Log("Player joined");

        // for object where cursor + input are in one
        //player.transform.SetParent(FindObjectOfType<Canvas>().transform, false);

        DontDestroyOnLoad(player);

        eventQueue.AddEvent(new ControllerJoinedEventData(player));
    }
}
