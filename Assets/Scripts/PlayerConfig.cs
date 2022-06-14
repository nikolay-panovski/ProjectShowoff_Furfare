using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfig
{
    public PlayerInput input { get; set; }
    public GameObject gameObject { get { return input.gameObject; } }
    public int playerIndex { get; set; }
    public UICursorSelector cursorObject { get; set; }
    public GameObject characterModel { get; set; }
    public bool isReady { get; set; }

    public PlayerConfig(PlayerInput controllerInput)
    {
        input = controllerInput;
    }
}
