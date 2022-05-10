using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] private Projectile _bulletPrefab;
    [SerializeField] private int _cooldownDuration = 1;
    private bool _canFire = false;

    private void SetCanFire(bool trueOrFalse)
    {
        _canFire = trueOrFalse;
    }

    private void Fire()
    {
        Projectile createdBullet = Instantiate<Projectile>(_bulletPrefab, transform.position + (transform.forward * 1.5f), Quaternion.identity);
        //Set own forward vector for now, replace with vector from the controller joystick
        createdBullet.SetDirection(transform.forward);
        SetCanFire(false);
    }

    private void ShootingInput()
    {
        //Add input trigger
        if (_canFire) Fire();
    }

    private void Update()
    {
        ShootingInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProjectilePickup") && _canFire == false)
        {
            _canFire = true;
            Destroy(other.gameObject);
        }
    }
}
