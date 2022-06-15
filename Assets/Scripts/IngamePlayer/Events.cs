using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    //public GameObject myX2;
    //public GameObject myReflexes;
    //public EventsSwitch es;
    //public bool shouldLog = false;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}
    //public void NoPowerOn()
    //{
    //    myReflexes.gameObject.SetActive(false);
    //    myX2.gameObject.SetActive(false);
    //    if (shouldLog)
    //    {
    //        Debug.Log("No Event On");
    //    }
    //    Invoke("NoPowerOff", 10f);
    //}
    //void NoPowerOff()
    //{
    //    if (shouldLog)
    //    {
    //        Debug.Log("No Event Off");
    //    }
    //    //es.ChooseEvent(0);
    //}

    //public void X2On()
    //{
    //    myReflexes.gameObject.SetActive(false);
    //    myX2.gameObject.SetActive(true);

    //    Player pl = this.GetComponent<Player>();
    //    pl.amountX = 200;
    //    if (shouldLog)
    //    {
    //        Debug.Log("AmountX: " + pl.amountX);

    //        Debug.Log("2x Event On");
    //    }
    //    Invoke("X2Off", 10f);
    //}
    //void X2Off()
    //{
    //    Player pl = this.GetComponent<Player>();
    //    pl.amountX = 100;
    //    if (shouldLog) { 
    //        Debug.Log("AmountX after wait: " + pl.amountX);
    //        Debug.Log("No Event Off");
    //    }
    //    //es.ChooseEvent(1);
    //}
    //public void ReflexesOn()
    //{
    //    if (shouldLog)
    //    {
    //        Debug.Log("ReflexesOn");
    //    }
    //    myReflexes.gameObject.SetActive(true);
    //    myX2.gameObject.SetActive(false);

    //    SimpleMoveController pl = this.GetComponent<SimpleMoveController>();
    //    pl.speedX = 2;
    //    if (shouldLog)
    //    {
    //        Debug.Log("SpeedX: " + pl.speedX);
    //        Debug.Log("Fast Reflexes Event On");
    //    }
    //    Invoke("ReflexesOff", 10f);
    //}
    //void ReflexesOff()
    //{
    //    SimpleMoveController pl = this.GetComponent<SimpleMoveController>();
    //    pl.speedX = 1;
    //    if (shouldLog)
    //    {
    //        Debug.Log("SpeedX after wait: " + pl.speedX);
    //        Debug.Log("Fast Reflexes Event Off");
    //    }
    //    //es.ChooseEvent(2);
    //}
}
/* OLD VERSION OF THIS SCRIPT, I FIRST TRIED MAKING IT A SEPARATE GAMEOBJECT
     public GameObject[] players;
    public GameObject[] x2;
    public GameObject[] reflexes;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Choose(0));
    }
    IEnumerator Choose(int current)
    {
        int pwr = Random.Range(0, 3);
        Debug.Log("pwr = "+pwr);
        if(pwr == current)
        {
            Debug.Log("pwr is the same as before: " + pwr);
            StartCoroutine(Choose(pwr));
        }
        else
        {
            if (pwr == 0)
            {
                for (int j = 0; j < x2.Length; j++)
                {
                    reflexes[j].gameObject.SetActive(false);
                    x2[j].gameObject.SetActive(false);
                }
                Debug.Log("No Event On");
                yield return new WaitForSeconds(10);
                Debug.Log("No Event Off");
                StartCoroutine(Choose(0));
            }
            if (pwr == 1)
            {
                for (int j = 0; j < x2.Length; j++)
                {
                    reflexes[j].gameObject.SetActive(false);
                    x2[j].gameObject.SetActive(true);
                }
                for (int i = 0; i < players.Length; i++)
                {
                    Player pl = players[i].GetComponent<Player>();
                    pl.amountX = pl.amountX * 2;
                    Debug.Log("AmountX: " + pl.amountX);
                    Debug.Log("2x Event On");
                }
                yield return new WaitForSeconds(10);
                for (int z = 0; z < players.Length; z++)
                {
                    Player pl = players[z].GetComponent<Player>();
                    pl.amountX = pl.amountX / 2;
                    Debug.Log("AmountX after wait: " + pl.amountX);
                    Debug.Log("No Event Off");
                    StartCoroutine(Choose(1));
                }
            }
            if (pwr == 2)
            {
                for (int j = 0; j < x2.Length; j++)
                {
                    reflexes[j].gameObject.SetActive(true);
                    x2[j].gameObject.SetActive(false);
                }
                SimpleMoveController pl = players[3].GetComponent<SimpleMoveController>();
                pl.speedX = pl.speedX * 2;
                Debug.Log("SpeedX: " + pl.speedX);
                Debug.Log("Fast Reflexes Event On"); 

                yield return new WaitForSeconds(10);

                pl.speedX = pl.speedX / 2;
                Debug.Log("SpeedX after wait: " + pl.speedX);
                Debug.Log("Fast Reflexes Event Off");
                StartCoroutine(Choose(2));
            }
        }
    }
 */
/* V2 OF THE CODE (ATTACHED TO THE PLAYER)
 public GameObject myX2;
    public GameObject myReflexes;
    public EventsSwitch es;
    public bool shouldLog = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public IEnumerator NoPowerOn()
    {
        myReflexes.gameObject.SetActive(false);
        myX2.gameObject.SetActive(false);
        if (shouldLog)
        {
            Debug.Log("No Event On");
        }
        yield return new WaitForSeconds(10);
        NoPowerOff();
    }
    void NoPowerOff()
    {
        if (shouldLog)
        {
            Debug.Log("No Event Off");
        }
        es.ChooseEvent(0);
    }

    public IEnumerator X2On()
    {
        myReflexes.gameObject.SetActive(false);
        myX2.gameObject.SetActive(true);

        Player pl = this.GetComponent<Player>();
        pl.amountX = 200;
        if (shouldLog)
        {
            Debug.Log("AmountX: " + pl.amountX);

            Debug.Log("2x Event On");
        }
        yield return new WaitForSeconds(10);
        X2Off(pl);
    }
    void X2Off(Player pl)
    {
        pl.amountX = 100;
        if (shouldLog) { 
            Debug.Log("AmountX after wait: " + pl.amountX);
            Debug.Log("No Event Off");
        }
        es.ChooseEvent(1);
    }
    public IEnumerator ReflexesOn()
    {
        if (shouldLog)
        {
            Debug.Log("ReflexesOn");
        }
        myReflexes.gameObject.SetActive(true);
        myX2.gameObject.SetActive(false);

        SimpleMoveController pl = this.GetComponent<SimpleMoveController>();
        pl.speedX = 2;
        if (shouldLog)
        {
            Debug.Log("SpeedX: " + pl.speedX);
            Debug.Log("Fast Reflexes Event On");
        }

        yield return new WaitForSeconds(10);
        ReflexesOff(pl);
    }
    void ReflexesOff(SimpleMoveController pl)
    {
        pl.speedX = 1;
        if (shouldLog)
        {
            Debug.Log("SpeedX after wait: " + pl.speedX);
            Debug.Log("Fast Reflexes Event Off");
        }
        es.ChooseEvent(2);
    }
 */
