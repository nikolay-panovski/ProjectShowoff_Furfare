using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerPodium : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerPositions;

    private List<PlayerConfig> players = new List<PlayerConfig>();

    // Start is called before the first frame update
    void Start()
    {
        GetAllPlayers();
        CalculateRankings();
        SpawnPlayerModels();
    }

    private void GetAllPlayers()
    {
        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            players.Add(PlayerManager.Instance.GetPlayerAtIndex(i));
        }
    }

    private void CalculateRankings()
    {
        for (int i = 0; i < players.Count - 1; i++)
        {
            PlayerConfig player0 = players[i];
            PlayerConfig player1 = players[i + 1];
            if (player0.score > player1.score) i = 0;
            players.Sort((player0, player1) => player0.score.CompareTo(player1.score));
        }
        players.Reverse();
    }

    private void SpawnPlayerModels()
    {
        Debug.Log(players.Count);
        for (int i = 0; i < players.Count; i++)
        {
            GameObject createdModel = Instantiate<GameObject>(players[i].characterModel, _playerPositions[i].transform.position, Quaternion.identity);
            createdModel.transform.localScale = new Vector3(1, 1, 1);
            createdModel.transform.Rotate(0, 90, 0);
        }
    }
}
