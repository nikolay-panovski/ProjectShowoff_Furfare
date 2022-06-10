using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal _linkedPortal;
    [SerializeField] private float _cooldownDuration = 2;
    private bool _active = true;

    public void ToggleActive()
    {
        _active = !_active;
        //Turns the portal back on after a cooldown
        if (_active == false) Invoke("ToggleActive", _cooldownDuration);
    }

    public Vector3 GetLinkedPortalPosition()
    {
        Vector3 location = new Vector3(0, 0, 0);
        if (_linkedPortal != null) location = _linkedPortal.transform.position;
        return location;
    }

    public bool GetActiveStatus()
    {
        return _active;
    }

    public Portal GetLinkedPortal()
    {
        return _linkedPortal;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.GetComponent<AudioSource>().Play();
        }
    }
}
