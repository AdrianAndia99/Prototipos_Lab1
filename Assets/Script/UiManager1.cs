using UnityEngine;
using TMPro;

public class UiManager1 : MonoBehaviour
{
    public TextMeshProUGUI ActualScore;
    public TextMeshProUGUI HighScore;

    public void UpdateScore(int score)
    {
        if (ActualScore != null)
        {
            ActualScore.text = "Score: " + score;

        }
    }
    public void UpdateHighScore(int highScore)
    {
        if (HighScore != null)
        {
            HighScore.text = "High Score: " + highScore;
        }
    }
}