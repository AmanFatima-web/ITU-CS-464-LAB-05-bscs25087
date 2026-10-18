
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int targetScore = 10;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text bonusCoinsText;
    [SerializeField] GameObject winPanel;

    int score;
    int bonusCoins;
    int bonusTarget = 50;

    public int GetScore()
    {
        return score;
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        if (winPanel != null)
            winPanel.SetActive(false);

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();

        if (score >= targetScore)
            Win();
    }

    public void AddBonusCoin()
    {
        if (bonusCoins >= bonusTarget)
            return;

        bonusCoins++;
        UpdateUI();

        if (bonusCoins >= bonusTarget)
            Win();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score} / {targetScore}";

        if (bonusCoinsText != null)
            bonusCoinsText.text = $"BonusCoins: {bonusCoins}/{bonusTarget}";
    }

    void Win()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
