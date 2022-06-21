using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private EventQueue eventQueue;

    [SerializeField] private Text[] _allScoreText;
    private int[] _allScores = new int[] { 0, 0, 0, 0 };

    public void IncreaseScore(int playerID, int amount)
    {
        _allScores[playerID] += amount;
        _allScoreText[playerID].text = _allScores[playerID] + "";
    }
}
