using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    private EventQueue eventQueue;

    [SerializeField] int _pickupSpawnInterval = 10;
    [SerializeField] int _maximumPickups = 4;

    private List<Spawnpoint> _allSpawnpoints = new List<Spawnpoint>();
    private int numSpawnedPickups = 0;

    // Start is called before the first frame update
    void Start()
    {
        eventQueue = FindObjectOfType<EventQueue>();
        eventQueue.Subscribe(EventType.SPAWNPOINT_INITTED, OnSpawnpointInitted);
        eventQueue.Subscribe(EventType.PICKUP_SPAWNED, OnPickupSpawned);
        eventQueue.Subscribe(EventType.PICKUP_PICKED, OnPickupPicked);
        Invoke("CheckforPickupSpawn", _pickupSpawnInterval);
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.SPAWNPOINT_INITTED, OnSpawnpointInitted);
        eventQueue.Unsubscribe(EventType.PICKUP_SPAWNED, OnPickupSpawned);
        eventQueue.Unsubscribe(EventType.PICKUP_PICKED, OnPickupPicked);
    }

    private void OnSpawnpointInitted(EventData eventData)
    {
        SpawnpointInittedEventData data = (SpawnpointInittedEventData)eventData;
        _allSpawnpoints.Add(data.spawnpoint);
    }

    private void OnPickupSpawned(EventData eventData)
    {
        numSpawnedPickups++;
    }

    private void OnPickupPicked(EventData eventData)
    {
        numSpawnedPickups--;
    }

    private void CheckforPickupSpawn()
    {
        if (numSpawnedPickups < _maximumPickups) PickSpawnpoint();
        Invoke("CheckforPickupSpawn", _pickupSpawnInterval);
    }

    private void PickSpawnpoint()
    {
        List<Spawnpoint> availableSpawnpoints = new List<Spawnpoint>();
        for (int i = 0; i < _allSpawnpoints.Count; i++)
        {
            if (_allSpawnpoints[i].hasSpawnedPickup == false) availableSpawnpoints.Add(_allSpawnpoints[i]);
        }
        int randomNumber = Random.Range(0, availableSpawnpoints.Count);
        availableSpawnpoints[randomNumber].SpawnPickup();
    }
}
