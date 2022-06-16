using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSounds : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ProjectilePickup")
        {
            this.GetComponent<AudioSource>().Play();
        }
    }
}
