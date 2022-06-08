using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class JoinPlayersHandle : MonoBehaviour
{
    /**
    [SerializeField] private GameObject playerPrefab = null;

    private PlayerInputManager manager = null;
    {
        ++InputUser.listenForUnpairedDeviceActivity;
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("Player joined");

        // child this InputObject to the canvas; its Cursor should be a child of this
        player.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        // TODO: Resolve hierarchy so this can indeed be DontDestroyOnLoad-ed
        DontDestroyOnLoad(player);

        // TODO: (Code quality) Send an event for GameManager to consume through EventSystem instead
        GameManager.Instance.AddPlayerInputObject(player);
    }
}
