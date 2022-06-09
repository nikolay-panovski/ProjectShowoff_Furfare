using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButtonClickHandle : MonoBehaviour
{
    
    void Update()
    {
        if (PlayerManager.Instance.GetAllPlayersReady())
        {
            GetComponent<Image>().enabled = true;
            GetComponent<Button>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<Image>().enabled = false;
            GetComponent<Button>().enabled = false; 
            GetComponent<BoxCollider2D>().enabled = false;
        }

        print(PlayerManager.Instance.GetAllPlayersReady());
    }
}
