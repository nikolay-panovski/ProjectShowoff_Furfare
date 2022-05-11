using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] Projectile _pickupPrefab;

    public bool _hasSpawnedPickup;

    public void SpawnPickup()
    {
        Projectile spawnedProjectile = Instantiate(_pickupPrefab, transform.position, Quaternion.identity);
        spawnedProjectile.SetOriginalSpawnpoint(this);
        SetHasSpawnedPickup(true);
    }

    public void SetHasSpawnedPickup(bool trueOrFalse)
    {
        _hasSpawnedPickup = trueOrFalse;
    }
}
