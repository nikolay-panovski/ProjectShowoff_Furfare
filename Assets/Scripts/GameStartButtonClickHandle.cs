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
            GetComponent<Button>().interactable = true;

            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            GetComponent<Image>().enabled = false;
            GetComponent<Button>().enabled = false;
            GetComponent<Button>().interactable = false;

            transform.GetChild(0).gameObject.SetActive(false);
        }

        print(PlayerManager.Instance.GetAllPlayersReady());
    }
}
