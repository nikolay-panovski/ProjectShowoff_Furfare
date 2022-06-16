using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    //Timer
    public float roundTime;
    public bool timeIsLeft;
    public Text timeText;

    //Places
    int[] Scores;
    int[] Places;
    int[] WeirdPlaces;
    [SerializeField] Sprite[] Sprites;
    [SerializeField] Image[] PlayerPlaces;

    //Infographic
    public GameObject InGameUIScreen;
    public GameObject Countdown;
    // Start is called before the first frame update
    void Start()
    {
        timeIsLeft = true;

        Scores = new int[4];
        Places = new int[4];
        WeirdPlaces = new int[4];
        Places[0] = 0;
        Places[1] = 1;
        Places[2] = 2;
        Places[3] = 3;
    }
    void Update()
    {
        timer();
    }

    void OrderPlaces()
    {
        bool Weird = false;
        Debug.Log(Places[0]+ " " + Places[1]+ " " + Places[2]+" "+ Places[3]);
        WeirdPlaces = Places;
        for (int i = 0; i < 3; i++)
        {
            if (Scores[i] == Scores[i + 1])
            {
                Debug.Log("Matching Scores: " + i + " " + (i+1) +"\n" + Scores[i]);
                int a = 0, b = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (Places[j] == i)
                    {
                        a = j;
                        Debug.Log("A is set!");
                    }
                    else if (Places[j] == i + 1)
                    {
                        b = j;
                        Debug.Log("B is set!");
                    }
                }
                Debug.Log(a + "\n" + b);
                Weird = true;
                WeirdPlaces[b] = WeirdPlaces[a];
            }
        }
        if (Weird)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerPlaces[i].sprite = Sprites[WeirdPlaces[i]];
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerPlaces[i].sprite = Sprites[Places[i]];
            }
        }
    }
    public void UpdatePlace(int Score, int PlayerID)
    {
        Scores[Places[PlayerID]] = Score;
        for(int i = 2; i >= 0 ; i--)
        {
            if (Scores[i + 1] > Scores[i])
            {
                int a = 0, b = 0;
                for (int j = 3; j >= 0; j--)
                {
                    if (Places[j] == i)
                    {
                        a = j;
                    }
                    else if (Places[j] == i + 1)
                    {
                        b = j;
                    }
                }
                int tmp = Places[a];
                Places[a] = Places[b];
                Places[b] = tmp;

                tmp = Scores[i];
                Scores[i] = Scores[i+1];
                Scores[i + 1] = tmp;
            }
        }
        OrderPlaces();
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

    public void StartCountDown()
    {
        InGameUIScreen.gameObject.SetActive(true);
        Countdown.gameObject.SetActive(true);
    }
}
