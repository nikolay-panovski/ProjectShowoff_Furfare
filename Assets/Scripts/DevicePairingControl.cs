using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

/* A class to exert control over the user/device pairing for players already in a scene
 * (not instantiated with PlayerInput.Instantiate).
 */
public class DevicePairingControl : MonoBehaviour
{
    private const int MAX_PLAYERS = 4;

    private PlayerInput[] playerInputs = new PlayerInput[MAX_PLAYERS];

    // Start is called before the first frame update
    void Start()
    {
        playerInputs = FindObjectsOfType<PlayerInput>();

        foreach (PlayerInput p in playerInputs)
        {
            p.user.UnpairDevices();

            if (p.currentControlScheme != p.defaultControlScheme)
            {
                p.SwitchCurrentControlScheme(p.defaultControlScheme);
            }
            if (p.currentControlScheme == "Keyboard&Mouse")
            {
                p.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
                continue;
            }

            performPairingWithDevice(p, p.defaultControlScheme);
        }
    }

    private void performPairingWithDevice(PlayerInput p, string defaultControlScheme)
    {
        switch (defaultControlScheme)
        {
            case "Keyboard&Mouse":
                Keyboard pairKeyboard = getUnpairedDeviceOfType<Keyboard>();
                if (pairKeyboard != null)
                {
                    InputUser.PerformPairingWithDevice(pairKeyboard, p.user);
                }
                else Debug.LogWarning("Unpaired device of type Keyboard not found. Is there a Keyboard connected to the computer?");
                break;
            case "Gamepad":
                Gamepad pairGamepad = getUnpairedDeviceOfType<Gamepad>();
                if (pairGamepad != null)
                {
                    InputUser.PerformPairingWithDevice(pairGamepad, p.user);
                }
                else Debug.LogWarning("Unpaired device of type Gamepad not found. Is there a Gamepad connected to the computer?");
                break;
            default:
                throw new System.ArgumentException("Bad string in defaultControlScheme. Check the naming in Unity and the switch in performPairingWithDevice!");
        }
    }

    private T getUnpairedDeviceOfType<T>() where T : InputDevice
    {
        foreach (InputDevice device in InputUser.GetUnpairedInputDevices())
        {
            if (device is T)
            {
                return (T)device;
            }
        }

        return null;
    }
}
