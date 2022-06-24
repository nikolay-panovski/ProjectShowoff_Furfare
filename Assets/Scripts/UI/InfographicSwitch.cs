using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfographicSwitch : MonoBehaviour
{
    [SerializeField] GameObject CharSelection;
    // Update is called once per frame
    public void InfographicPlay()
    {
        this.gameObject.SetActive(true);
        CharSelection.gameObject.SetActive(false);
    }
}
