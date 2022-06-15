using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfig
{
    public PlayerInput UIInput { get; set; }    // starting PlayerInput component attached to the InputObject instantiated prefab.
    public GameObject gameObject { get { return UIInput.gameObject; } }
    public int playerIndex { get; set; }    // index of the PlayerInput itself - the Nth player (in order) to join the game.
                                            // Should correspond to PlayerInput's user.id.
    public UICursorSelector cursorObject { get; set; }
    public GameObject characterModel { get; set; }  // UI character model (CharacterSelection screen).
    public int characterIndex { get; set; } // index of the character (WITH INPUTS) as part of the corresponding PlayerManager array.
    
    /// <summary>
    /// A ready player has selected a character and has a character model assigned.
    /// </summary>
    public bool isReady { get; set; }
    public PlayerInput gameplayInput { get; set; }  // PlayerInput component attached to the functional Player script of the whole player structure.

    public PlayerConfig(PlayerInput controllerInput)
    {
        UIInput = controllerInput;
    }
}
