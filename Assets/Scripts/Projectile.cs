using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int _speed = 100;
    [SerializeField] private int _maxBounces = 3;
    private int _bounceCount;

    public void SetDirection(Vector3 newDirection)
    {
        Rigidbody myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.AddForce(newDirection * Time.deltaTime * _speed);
    }

    private void CheckCollisionCount()
    {
        if (_bounceCount >= _maxBounces) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _bounceCount += 1;
        CheckCollisionCount();
    }
}
