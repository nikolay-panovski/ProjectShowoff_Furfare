using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class JoinPlayersHandle : MonoBehaviour
{
    /**
    [SerializeField] private GameObject playerPrefab = null;

    private PlayerInputManager manager = null;

    private void Awake()
    {
        ++InputUser.listenForUnpairedDeviceActivity;
        InputUser.onUnpairedDeviceUsed += onUnpairedDeviceUsed;

        if (!TryGetComponent<PlayerInputManager>(out manager)) Debug.LogError("A JoinPlayersHandle script is attached to a non-" +
            "PlayerInputManager object! Fix this!");
    }

    private void onUnpairedDeviceUsed(InputControl device, UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
    {
        // controlScheme should be Gamepad at the end
        manager.JoinPlayer(GameManager.Instance.joinedPlayers, controlScheme: "Keyboard&Mouse", pairWithDevice: (InputDevice)device);
        GameManager.Instance.joinedPlayers++;
    }
    /**/

    private void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("Player joined");

        DontDestroyOnLoad(player);

        // TODO: (Code quality) Send an event for GameManager to consume through EventSystem instead
        GameManager.Instance.AddPlayerInputObject(player);
    }
}
