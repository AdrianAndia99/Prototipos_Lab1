using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : SingletonNoPersistent<GameManager1>
{
    public ScoreDataSO scoreData;
    public UiManager1 uiManager;

    private void Awake()
    {
        scoreData.highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void OnEnable()
    {
        PlayerController1.OnFood += UpdateScore;
        PlayerController1.OnEnemy += LoseGame;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        PlayerController1.OnFood -= UpdateScore;
        PlayerController1.OnEnemy -= LoseGame;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        scoreData.currentScore = 0;

        if (uiManager != null)
        {
            uiManager.UpdateScore(scoreData.currentScore);
            uiManager.UpdateHighScore(scoreData.highScore);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        uiManager = FindObjectOfType<UiManager1>();

        if (uiManager != null)
        {
            uiManager.UpdateScore(scoreData.currentScore);
            uiManager.UpdateHighScore(scoreData.highScore);
        }
        else
        {
            Debug.LogWarning("UiManager no encontrado en la escena.");
        }
    }// profe pido perdón, pero este fue mi ultimo recurso XD 

    public void UpdateScore()
    {
        scoreData.currentScore++;
        uiManager?.UpdateScore(scoreData.currentScore);

        if (scoreData.currentScore > scoreData.highScore)
        {
            scoreData.highScore = scoreData.currentScore;
            PlayerPrefs.SetInt("HighScore", scoreData.highScore);
            PlayerPrefs.Save();
            uiManager?.UpdateHighScore(scoreData.highScore);
        }
    }

    public void LoseGame()
    {
        Debug.Log("¡Perdiste!");
        scoreData.currentScore = 0;
        ResetGame();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}