using System.Collections.Generic;
using UnityEngine;

public class PlayfieldSpawner : MonoBehaviour
{
    [Header("Width (in Unity units)")]
    [SerializeField] private int width;

    [Header("Height (in Unity units)")]
    [SerializeField] private int height;

    [Space(20)]
    [SerializeField] private List<GameObject> wallPrefabs;

    [SerializeField] private int numberOfWalls;

    public void GeneratePlayfield()
    {
        generateBorderAndFloor();
        generateInnerWalls();
    }

    private void generateBorderAndFloor()
    {
        GameObject leftWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        leftWall.transform.localScale = new Vector3(1, 1, height);
        leftWall.transform.SetPositionAndRotation(new Vector3(-width / 2, 0, 0), Quaternion.identity);

        GameObject rightWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightWall.transform.localScale = new Vector3(1, 1, height);
        rightWall.transform.SetPositionAndRotation(new Vector3(width / 2, 0, 0), Quaternion.identity);

        GameObject topWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topWall.transform.localScale = new Vector3(width, 1, 1);
        topWall.transform.SetPositionAndRotation(new Vector3(0, 0, -height / 2), Quaternion.identity);

        GameObject bottomWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottomWall.transform.localScale = new Vector3(width, 1, 1);
        bottomWall.transform.SetPositionAndRotation(new Vector3(0, 0, height / 2), Quaternion.identity);


        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.localScale = new Vector3(width, 1, height);
        floor.transform.SetPositionAndRotation(new Vector3(0, -1, 0), Quaternion.identity);
    }

    private void generateInnerWalls()
    {
        System.Random random = new System.Random();

        for (int i = 0; i < numberOfWalls; i++)
        {
            // TODO?: grid/snapping; coord clamping according to prefab (size) picked

            GameObject.Instantiate(wallPrefabs[random.Next(0, wallPrefabs.Count)],
                                   new Vector3(random.Next(-width / 2, width / 2), 0, random.Next(-height / 2, height / 2)),
                                   Quaternion.identity);
        }
    }
}
