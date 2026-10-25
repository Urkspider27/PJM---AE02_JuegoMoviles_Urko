using UnityEngine;
using TMPro;

public class TypingUIManager : MonoBehaviour
{
    [Header("UI TextElements")]
    public TMP_Text scoreText;
    public TMP_Text targetWordText;
    public TMP_Text inputWordText;

    private string currentTargetWord = "";
    private string currentInput = "";

    void Start()
    {
        GameManager.Instance.OnScoreUpdated += UpdateScoreUI;
        GameManager.Instance.OnTargetWordGenerated += SetTargetWord;
        GameManager.Instance.OnPlayerFailed += HandleGameOver;

        targetWordText.text = "";
        inputWordText.text = "";
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
    }

    public void AddLetter(string letter)
    {
        if (string.IsNullOrEmpty(currentTargetWord)) return;

        currentInput += letter.ToUpper();
        inputWordText.text = currentInput;

        if (currentInput == currentTargetWord)
        {
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
        targetWordText.text = "GAME OVER";
        Time.timeScale = 0f;
    }
}