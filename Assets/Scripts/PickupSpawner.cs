using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] int _pickupSpawnInterval = 10;
    [SerializeField] int _maximumPickups = 4;
    public Spawnpoint[] _allSpawnpoints;

    // Start is called before the first frame update
    void Start()
    {
        _allSpawnpoints = FindObjectsOfType<Spawnpoint>();
        Invoke("CheckforPickupSpawn", _pickupSpawnInterval);
    }

    private void CheckforPickupSpawn()
    {
        if (GameObject.FindGameObjectsWithTag("ProjectilePickup").Length < _maximumPickups) PickSpawnpoint();
        Invoke("CheckforPickupSpawn", _pickupSpawnInterval);
    }

    private void PickSpawnpoint()
    {
        List<Spawnpoint> availableSpawnpoints = new List<Spawnpoint>();
        for (int i = 0; i < _allSpawnpoints.Length; i++)
        {
            if (_allSpawnpoints[i]._hasSpawnedPickup == false) availableSpawnpoints.Add(_allSpawnpoints[i]);
        }
        int randomNumber = Random.Range(0, availableSpawnpoints.Count);
        Debug.Log(availableSpawnpoints.Count);
        availableSpawnpoints[randomNumber].SpawnPickup();
    }
}
