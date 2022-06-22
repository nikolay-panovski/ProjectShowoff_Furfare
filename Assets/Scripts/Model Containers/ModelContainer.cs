using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelContainer", menuName = "Containers/Model Container")]
public class ModelContainer : ScriptableObject
{
    public List<GameObject> models;
}