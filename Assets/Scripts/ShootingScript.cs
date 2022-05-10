using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    private Projectile heldProjectile;
    [SerializeField] private int _cooldownDuration = 1;
    private bool _canFire = false;

    private void SetCanFire(bool trueOrFalse)
    {
        _canFire = trueOrFalse;
    }

    private void Fire()
    {
        //Set own forward vector for now, replace with vector from the controller joystick
        heldProjectile.SetState(ProjectileState.FIRED);
        heldProjectile.SetDirection(transform.forward * 1.2f);
        SetCanFire(false);
        heldProjectile = null;
    }

    private void ShootingInput()
    {
        //input trigger through OnFire (technically this method is already useless)
        if (_canFire) Fire();
    }

    public void OnFire(InputValue value)
    {
        ShootingInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProjectilePickup") && _canFire == false)
        {
            _canFire = true;
            //Destroy(other.gameObject);
            pickProjectileUp(other.gameObject.GetComponent<Projectile>());     // dirty, should not be here
        }
    }

    private void pickProjectileUp(Projectile projectile)
    {
        heldProjectile = projectile;
        heldProjectile.SetHoldingPlayer(this.gameObject);
        heldProjectile.SetState(ProjectileState.HELD);
    }
}
