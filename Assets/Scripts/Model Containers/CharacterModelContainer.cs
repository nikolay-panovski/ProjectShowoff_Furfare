using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterContainer", menuName = "Containers/Character Container")]
public class CharacterModelContainer : ScriptableObject
{
    public List<GameObject> characterModels;
}