using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private EventQueue eventQueue;

    private static GameSceneManager instance = null;

    public static GameSceneManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        //SceneManager.activeSceneChanged += onSceneChanged;
        SceneManager.sceneLoaded += onSceneChanged;

        eventQueue = FindObjectOfType<EventQueue>();
    }

    private void onSceneChanged(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        SceneProperties loadedSceneProperties = FindObjectOfType<SceneProperties>();

        /// ON JOINING PLAYERS ALLOWED
        /**
        if (loadedSceneProperties.isJoiningPlayersAllowed == true)
        {
            findAndTogglePlayerInputManager(true);
        }
        else //if (loadedSceneProperties.isJoiningPlayersAllowed == false)
        {
            findAndTogglePlayerInputManager(false);
        }
        /**/
        findAndTogglePlayerInputManager(loadedSceneProperties.isJoiningPlayersAllowed); // same as above (commented out and hidden)

        /// ON INPUT IS FOR GAMEPLAY
        if (loadedSceneProperties.isInputForGameplay == true)
        {
            toggleExistingPlayerCursors(false);
            togglePlayersUIInput(false);
            //an instantiateFunctionalPlayers() + set gameplayInput references - a job of PlayerSpawnpoint s:
            eventQueue.AddEvent(new PlayersEnteringGameplayEventData(PlayerManager.Instance.numJoinedPlayers));
        }
        else //if (loadedSceneProperties.isInputForGameplay == false)
        {
            toggleExistingPlayerCursors(true);
            togglePlayersUIInput(true);
            // ~~not consistent with above, but skip events and invalidate gameplay inputs on gameplay exit ourselves:
            setPlayersGameplayInput(null);
        }

        /// ON RESET PLAYERS READY STATE
        if (loadedSceneProperties.shouldResetPlayersReadyState == true)
        {
            resetPlayersReadyState();
        }

        /// ON RESET SCORES
        if (loadedSceneProperties.shouldResetScores == true)
        {
            // UNDEFINED - players do not preserve scores (yet)
        }
        else //if (loadedSceneProperties.shouldResetScores == false)
        {
            // UNDEFINED - players do not preserve scores (yet)
        }

        // happens on EACH new scene load, unless filtered:
        //print(loadedScene.name);
    }

    private void findAndTogglePlayerInputManager(bool activeState)
    {
        // ~~can change to hold reference to it instead since it is DontDestroyOnLoad, if such optimization is needed
        FindObjectOfType<PlayerInputManager>(true).gameObject.SetActive(activeState);
    }

    private void toggleExistingPlayerCursors(bool activeState)
    {
        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            PlayerConfig player = PlayerManager.Instance.GetPlayerAtIndex(i);
            player.cursorObject.gameObject.SetActive(activeState);
        }
    }

    private void togglePlayersUIInput(bool activeState)
    {
        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            PlayerConfig player = PlayerManager.Instance.GetPlayerAtIndex(i);
            player.UIInput.enabled = activeState;
        }
    }

    private void resetPlayersReadyState()
    {
        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            PlayerConfig player = PlayerManager.Instance.GetPlayerAtIndex(i);
            player.isReady = false;
        }
    }

    private void setPlayersGameplayInput(PlayerInput input)
    {
        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            PlayerConfig player = PlayerManager.Instance.GetPlayerAtIndex(i);
            player.gameplayInput = input;
        }
    }
}
