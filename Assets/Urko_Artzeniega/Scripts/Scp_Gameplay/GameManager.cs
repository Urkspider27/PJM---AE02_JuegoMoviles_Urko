using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Patrón Observer (la UI escuchará estos eventos)
    public Action<int> OnScoreUpdated;
    public Action<string> OnTargetWordGenerated;
    public Action OnPlayerFailed;

    private IScoreService scoreService;
    private int currentScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        scoreService = GetComponent<IScoreService>();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        OnScoreUpdated?.Invoke(currentScore);
        scoreService.SaveHighScore(currentScore);
    }

    public void GenerateNewObstacleWord(string newWord)
    {
        OnTargetWordGenerated?.Invoke(newWord);
        // Aquí se activará la cámara lenta más adelante
    }

    public void TriggerGameOver()
    {
        OnPlayerFailed?.Invoke();
        // Lógica de reinicio
    }
}