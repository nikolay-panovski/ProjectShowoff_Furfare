using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_InGameCardSpawner : MonoBehaviour
{
    [Tooltip("1 through 8, in order to spawn the correponding character from the order of Character Selection screen.")]
    [SerializeField] private int cardIDToSpawn;
    [SerializeField] private PlayerInput playerInput;

    private void Start()
    {
        PlayerConfig player = new PlayerConfig(playerInput);

        // mandatory for the visual UI
        player.playerUICard = Instantiate(
            PlayerManager.Instance.GetCardSpriteAtIndex(cardIDToSpawn - 1), this.transform);

        PlayerManager.Instance.UpdatePlayerListWithJoiningManual(player);

        // optional, only if the mock player also has actual PlayerInput attached and will do anything
        if (playerInput != null)
        {
            player.player = playerInput.GetComponent<Player>();
            player.playerUICard.GetComponent<AssignProjectileIcons>().SetConnectedPlayer(player);
        }
    }
}
