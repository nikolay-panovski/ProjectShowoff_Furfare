using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    //Timer


    //Places
    int[] Scores;
    int[] Places;
    int[] WeirdPlaces;
    [SerializeField] Sprite[] Sprites;
    [SerializeField] Image[] PlayerPlaces;
    // Start is called before the first frame update
    void Start()
    {
        Scores = new int[4];
        Places = new int[4];
        WeirdPlaces = new int[4];
        Places[0] = 0;
        Places[1] = 1;
        Places[2] = 2;
        Places[3] = 3;
    }
    void OrderPlaces()
    {
        bool Weird = false;
        WeirdPlaces = Places;
        for (int i = 0; i < 3; i++)
        {
            if (Scores[i] == Scores[i + 1])
            {
                Weird = true;
                WeirdPlaces[i + 1] = WeirdPlaces[i];
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
                Places[a] = Places[b] ^ Places[a];
                Places[b] = Places[a] ^ Places[b];
                Places[a] = Places[a] ^ Places[b];

                Scores[i] = Scores[i + 1] ^ Scores[i];
                Scores[i + 1] = Scores[i] ^ Scores[i + 1];
                Scores[i] = Scores[i] ^ Scores[i + 1];
            }
        }
        OrderPlaces();
    }
}
