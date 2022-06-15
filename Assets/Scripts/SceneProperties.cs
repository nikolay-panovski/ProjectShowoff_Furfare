using UnityEngine;

/* Script holding properties about a scene that indicate whether objects should be enabled, disabled,
 * or other actions should happen.
 * 
 * Attach a GameObject with this script to every scene that deals with handling gameplay and non-gameplay
 * components/objects (in practice, this should be about every used scene).
 */
public class SceneProperties : MonoBehaviour
{
    [Header("Functional properties")]
    [Header("(hover over first bool for more info)")]
    [Tooltip("These properties are to be set up for each scene, so that global objects (Player Manager) can easily perform " +
        "actions when a new scene loads (such as: switching between UI and gameplay inputs, toggling cursors, resetting scores).")]

    // one bool takes care of all differences?
    //[SerializeField] private bool gameplayScene;    // Functional players, Score references (true)
    // vs Cursors, PlayerInputManager (false)

    // OR, split bools per task, even if there turn out to be no differences other than gameplay<->non-gameplay?
    [SerializeField] private bool allowPlayersToJoin;  // PlayerInputManager / cursor instantiation
    [SerializeField] private bool inputForGameplay;     // use starting PlayerInput component with UI controls vs
                                                        // player-attached one with gameplay controls
                                                        // respectively enable/disable cursors
    [SerializeField] private bool resetPlayersReadyState;
    [SerializeField] private bool resetScores;          // define at start of which scene players scores will stop being preserved

    #region PUBLIC GETTERS
    // Note: Cannot public get above, because of the serialization involved.
    /**
    public bool isGameplayScene
    {
        get { return gameplayScene; }
    }
    /**/

    public bool isJoiningPlayersAllowed
    {
        get { return allowPlayersToJoin; }
    }

    public bool isInputForGameplay
    {
        get { return inputForGameplay; }
    }

    public bool shouldResetPlayersReadyState
    {
        get { return resetPlayersReadyState; }
    }

    public bool shouldResetScores
    {
        get { return resetScores; }
    }
    #endregion
}
