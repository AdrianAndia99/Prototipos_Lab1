using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game Data/Score Data")]
public class ScoreDataSO : ScriptableObject
{
    public int currentScore = 0;
    public int highScore = 0;
}