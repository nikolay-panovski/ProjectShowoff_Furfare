using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Slider blue;
    public Slider green;
    public Slider pink;
    public Slider yellow;
    // Start is called before the first frame update

    public void IncreaseScore(int playerNumber)
    {
        if (playerNumber == 1) blue.value += 1f;
        if (playerNumber == 2) green.value += 1f;
        if (playerNumber == 3) pink.value += 1f;
        if (playerNumber == 4) yellow.value += 1f;
    }
}
