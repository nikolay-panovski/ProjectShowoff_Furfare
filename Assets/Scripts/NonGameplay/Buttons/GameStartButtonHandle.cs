using UnityEngine;
using UnityEngine.UI;

public class GameStartButtonHandle : MonoBehaviour
{
    private Image displayImage;
    private Button functionalButton;
    private BoxCollider2D buttonCollider;

    private void Awake()
    {
        if (!TryGetComponent<Image>(out displayImage))
            throw new MissingComponentException("Button is missing an Image component! It will not display visually!");
        if (!TryGetComponent<Button>(out functionalButton))
            throw new MissingComponentException("Button is missing a functional Button component! It will not respond on press!");
        if (!TryGetComponent<BoxCollider2D>(out buttonCollider))
            throw new MissingComponentException("Button is missing a BoxCollider2D component! It will not respond on press!");
    }

    void Update()
    {
        enableOnPlayersReady();
    }

    private void enableOnPlayersReady()
    {
        if (PlayerManager.Instance.GetAllPlayersReady())
        {
            displayImage.enabled = true;
            functionalButton.enabled = true;
            buttonCollider.enabled = true;
        }
        else
        {
            displayImage.enabled = false;
            functionalButton.enabled = false;
            buttonCollider.enabled = false;
        }

        //print(PlayerManager.Instance.GetAllPlayersReady());
    }
}
