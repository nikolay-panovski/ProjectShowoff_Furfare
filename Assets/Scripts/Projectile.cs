using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int _speed = 100;
    public void SetDirection(Vector3 newDirection)
    {
        Rigidbody myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.AddForce(newDirection * Time.deltaTime * _speed);
    }
}
