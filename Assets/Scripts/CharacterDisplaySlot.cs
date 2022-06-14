using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplaySlot : MonoBehaviour
{
    [Tooltip("The 1-based index of the player whose character should be displayed here once selected (1 = player 1 etc.)")]
    [SerializeField] private int playerNumber;

    private GameObject displayedCharacter = null;

    // ~~Update is doing all of the work as usual
    void Update()
    {
        PlayerConfig player = PlayerManager.Instance.GetPlayerAtIndex(playerNumber - 1);
        if (player != null)
        {
            if (player.characterModel != null && displayedCharacter == null)
            {
                displayedCharacter = Instantiate(player.characterModel, this.transform);
            }

            if (player.characterModel == null && displayedCharacter != null)
            {
                Destroy(displayedCharacter);
                displayedCharacter = null;
            }
        }

        // TODO: Idle animation (unless it plays by default)
    }
}
