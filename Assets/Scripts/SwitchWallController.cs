using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWallController : MonoBehaviour
{
    [SerializeField] private int _expansionTimer;
    private SwitchWall[] _allSwitchWalls;

    // Start is called before the first frame update
    void Start()
    {
        _allSwitchWalls = FindObjectsOfType<SwitchWall>();
        Invoke("ToggleExpansionWalls", _expansionTimer);
    }

    private void ToggleExpansionWalls()
    {
        for (int i = 0; i < _allSwitchWalls.Length; i++)
        {
            _allSwitchWalls[i].ToggleExpansion();
        }
        Invoke("ToggleExpansionWalls", _expansionTimer);
    }
}