
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
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text finalBonusText;

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

        if (bonusCoinsText != null)
            bonusCoinsText.gameObject.SetActive(false);

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (score >= targetScore)
            return;

        score += amount;

        if (score > targetScore)
            score = targetScore;

        UpdateUI();

        if (score >= targetScore)
            Win();
    }

    public void AddBonusCoin()
    {
        if (bonusCoins >= bonusTarget)
            return;

        if (bonusCoinsText != null)
            bonusCoinsText.gameObject.SetActive(true);

        bonusCoins++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}/{targetScore}";

        if (bonusCoinsText != null)
            bonusCoinsText.text = $"BonusCoins: {bonusCoins}/{bonusTarget}";
    }


    void Win()
    {
        if (bonusCoinsText != null)
            bonusCoinsText.gameObject.SetActive(false);

        if (winPanel != null)
        {
            winPanel.SetActive(true);

            if (finalScoreText != null)
                finalScoreText.text = $"Score: {score}/{targetScore}";

            if (finalBonusText != null)
                finalBonusText.text = $"BonusCoins: {bonusCoins}/{bonusTarget}";
        }

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
