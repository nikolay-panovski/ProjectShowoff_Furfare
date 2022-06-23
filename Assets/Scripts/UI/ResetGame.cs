using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    private List<PlayerConfig> _allPlayers = new List<PlayerConfig>();

    private void GetAllPlayers()
    {
        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            _allPlayers.Add(PlayerManager.Instance.GetPlayerAtIndex(i));
        }
    }

    private void ResetInformation()
    {
        GetAllPlayers();

        for (int i = 0; i < _allPlayers.Count; i++)
        {
            _allPlayers[i].score = 0;
            _allPlayers[i].isReady = false;
        }
    }


}
