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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BlueIncrement() 
    {
        blue.value += 0.01f;
    }
    public void GreenIncrement()
    {
        green.value += 0.01f;
    }
    public void PinkIncrement()
    {
        pink.value += 0.01f;
    }
    public void YellowIncrement()
    {
        yellow.value += 0.01f;
    }
}
