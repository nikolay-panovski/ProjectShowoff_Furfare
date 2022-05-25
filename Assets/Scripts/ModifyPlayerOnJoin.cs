using UnityEngine;
using UnityEngine.InputSystem;

public class ModifyPlayerOnJoin : MonoBehaviour
{
    private void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("Player joined");

        DontDestroyOnLoad(player);
    }
}
