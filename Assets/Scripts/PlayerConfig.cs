using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfig
{
    public int score { get; set; }
    public PlayerInput UIInput { get; set; }    // starting PlayerInput component attached to the InputObject instantiated prefab.
                                                // If it is needed to access a GameObject/transform of something for parenting or
                                                // otherwise positioning, access this one's gameObject property explicitly.
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
    public int score { get; set; }

    public PlayerConfig(PlayerInput controllerInput)
    {
        UIInput = controllerInput;
    }
}
