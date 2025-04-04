using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Stats")]
    public int currentWave = 1;
    public int score = 0;
    public float timeElapsed = 0f;
    private bool isGameOver = false;

    [Header("UI References")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (!isGameOver)
        {
            timeElapsed += Time.deltaTime;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (waveText != null) waveText.text = $"Wave: {currentWave}";
        if (scoreText != null) scoreText.text = $"Score: {score}";
        if (timeText != null) timeText.text = $"Time: {timeElapsed:F1}s";
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void SetWave(int wave)
    {
        currentWave = wave;
        UpdateUI();
    }

    public void GameOver()
    {
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = $"Score: {score}";

        if (finalTimeText != null)
            finalTimeText.text = $"Time: {timeElapsed:F1}s";
    }

    // Кнопка "Сыграть заново"
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Кнопка "Главное меню"
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Кнопка "Выйти"
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // для редактора
#endif
        Debug.Log("Игра завершена.");
    }
}
