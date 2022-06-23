using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class JoinPlayersHandle : MonoBehaviour
{
    private EventQueue eventQueue;

    private static JoinPlayersHandle instance = null;

    public static JoinPlayersHandle Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        if (eventQueue == null) eventQueue = FindObjectOfType<EventQueue>();

        DontDestroyOnLoad(gameObject);  // Player Input Manager gameobject
    }

    private void OnPlayerJoined(PlayerInput input)
    {
        // try prevent already joined players from re-joining (do not let them fire a new event)
        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            PlayerConfig player = PlayerManager.Instance.GetPlayerAtIndex(i);
            if (player.UIInput == input) return;
        }

        Debug.Log("Player joined");

        // for object where cursor + input are in one
        //player.transform.SetParent(FindObjectOfType<Canvas>().transform, false);

        DontDestroyOnLoad(input);

        eventQueue.AddEvent(new ControllerJoinedEventData(input));
    }
}
