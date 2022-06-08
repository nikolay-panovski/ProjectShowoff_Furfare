using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Singleton game manager.
 * Rework the singleton part?
 */
public class GameManager : MonoBehaviour
{
    private List<PlayerInput> joinedPlayerInputs = new List<PlayerInput>();
    //private List<PlayerConfig> players = new List<PlayerConfig>();

    private int joinedPlayers = 0;

    private static GameManager instance = null;

    public static GameManager Instance
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
        }
        else
        {
            instance = this;
        }
    }

    public void AddPlayerInputObject(PlayerInput input)
    {
        joinedPlayerInputs.Add(input);
        joinedPlayers++;
    }
}
