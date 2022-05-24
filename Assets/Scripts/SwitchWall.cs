using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWall : MonoBehaviour
{
    [Tooltip("How many frames it takes to fully expand")]
    [SerializeField] private bool _expanded = false;
    [SerializeField] private float _heightDifference = 1f;
    [SerializeField] private float _extendSpeed = 1;
    private Vector3 _originalPosition;


    private void Start()
    {
        _originalPosition = transform.position;
    }

    public void ToggleExpansion()
    {
        _expanded = !_expanded;
    }

    private void AttemptExpanding()
    {
        if (_expanded == true && transform.position.y <= (_originalPosition.y + _heightDifference))
        {
            if (CheckForPlayerCollision() == false) transform.position += new Vector3(0, _extendSpeed * Time.deltaTime, 0);
        }

        else if (_expanded == false && transform.position.y >= _originalPosition.y)
            {
            transform.position -= new Vector3(0, _extendSpeed * Time.deltaTime, 0);
        }
    }

    private bool CheckForPlayerCollision()
    {
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Player")) return true;
            }
        }
        return false;
    }

    private void Update()
    {
        AttemptExpanding();
    }
}
