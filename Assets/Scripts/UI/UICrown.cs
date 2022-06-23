using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICrown : MonoBehaviour
{
    private Player player;
    private Camera thisCamera;

    void Start()
    {
        player = GetComponentInParent<Player>();

        thisCamera = Camera.main;
    }

    void Update()
    {
        //transform.position = player.transform.position + new Vector3(0, 5, 0);

        //transform.LookAt(thisCamera.transform);
        //transform.rotation = thisCamera.transform.rotation;
        transform.SetPositionAndRotation(player.transform.position + new Vector3(0, 5, 0), thisCamera.transform.rotation);

        //transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
    }
}
