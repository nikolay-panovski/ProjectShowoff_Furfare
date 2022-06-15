using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class InstantiateEventSystem : MonoBehaviour
{
    [Tooltip("Prefab including a MultiplayerEventSystem to pass. The whole prefab gets instantiated (so ideally include a UI Input Module to it too), but the MultiplayerEventSystem is requested as to not pass any random GameObject.")]
    [SerializeField] private MultiplayerEventSystem eventSystemPrefab;

    private void Awake()
    {
        GameObject newEventSystem = Instantiate(eventSystemPrefab.gameObject, this.transform);

        setupEventSystem(newEventSystem.GetComponent<MultiplayerEventSystem>());
    }

    // another dirty script
    /// <summary>
    /// Sets the First Selected and Player Root fields of the MultiplayerEventSystem to the first
    /// Button and Canvas objects found in the hierarchy respectively. This is used because in the current
    /// game setup it should not matter much which button is selected, and there should be only one Canvas
    /// per scene on which all players select options. These fields cannot be set in the InputObject Prefab itself.
    /// </summary>
    private void setupEventSystem(MultiplayerEventSystem system)
    {
        system.firstSelectedGameObject = GameObject.FindObjectOfType<Button>().gameObject;
        system.playerRoot = GameObject.FindObjectOfType<Canvas>().gameObject;
    }
}
