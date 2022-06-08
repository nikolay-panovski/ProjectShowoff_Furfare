using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float roundTime;
    public bool timeIsLeft;
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeIsLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer();
    }
    void timer()
    {
        if (timeIsLeft)
        {
            if (roundTime > 0)
            {
                roundTime -= Time.deltaTime;
                DisplayTime(roundTime);
            }
            else
            {
                //game ends
                Debug.Log("Time Ran out");
                roundTime = 0;
                timeIsLeft = false;
            }
            //MyPointsText.text = time;
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
