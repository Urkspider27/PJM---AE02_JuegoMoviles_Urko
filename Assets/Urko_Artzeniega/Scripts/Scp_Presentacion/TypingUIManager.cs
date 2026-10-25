using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypingUIManager : MonoBehaviour
{
    [Header("UI TextElements")]
    public TMP_Text scoreText;
    public TMP_Text targetWordText;
    public TMP_Text inputWordText;

    [Header("UI Buttons")]
    public Button restartButton;

    private string currentTargetWord = "";
    private string currentInput = "";
    private bool isWaitingForInput = false;

    void Start()
    {
        GameManager.Instance.OnScoreUpdated += UpdateScoreUI;
        GameManager.Instance.OnTargetWordGenerated += SetTargetWord;
        GameManager.Instance.OnPlayerFailed += HandleGameOver;

        targetWordText.text = "";
        inputWordText.text = "";

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreUpdated -= UpdateScoreUI;
            GameManager.Instance.OnTargetWordGenerated -= SetTargetWord;
            GameManager.Instance.OnPlayerFailed -= HandleGameOver;
        }
    }

    void SetTargetWord(string word)
    {
        currentTargetWord = word;
        currentInput = "";
        targetWordText.text = currentTargetWord;
        inputWordText.text = "";
        isWaitingForInput = true;
    }

    public void AddLetter(string letter)
    {
        if (!isWaitingForInput || string.IsNullOrEmpty(currentTargetWord)) return;

        currentInput += letter.ToUpper();
        inputWordText.text = currentInput;

        if (currentInput == currentTargetWord)
        {
            isWaitingForInput = false;
            GameManager.Instance.AddScore(10);

            FindFirstObjectByType<PlayerController>().Jump();

            currentTargetWord = "";
            targetWordText.text = "¡BIEN!";
            inputWordText.text = "";
        }
        else if (!currentTargetWord.StartsWith(currentInput))
        {
            currentInput = "";
            inputWordText.text = "";
        }
    }

    void UpdateScoreUI(int score)
    {
        scoreText.text = "PUNTOS: " + score;
    }

    void HandleGameOver()
    {
        isWaitingForInput = false;
        targetWordText.text = "GAME OVER";
        targetWordText.color = Color.red;

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}