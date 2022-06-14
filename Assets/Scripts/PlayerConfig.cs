using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfig
{
    public PlayerInput input { get; set; }
    public GameObject gameObject { get { return input.gameObject; } }
    public int playerIndex { get; set; }    // index of the PlayerInput itself - the Nth player (in order) to join the game.
                                            // Should correspond to PlayerInput's user.id.
    public UICursorSelector cursorObject { get; set; }
    public GameObject characterModel { get; set; }
    public int characterIndex { get; set; } // index of the character (WITH INPUTS) as part of the corresponding PlayerManager array.
    public bool isReady { get; set; }

    public PlayerConfig(PlayerInput controllerInput)
    {
        input = controllerInput;
    }
}
