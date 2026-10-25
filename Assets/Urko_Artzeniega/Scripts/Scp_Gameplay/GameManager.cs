using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Action<int> OnScoreUpdated;
    public Action<string> OnTargetWordGenerated;
    public Action OnPlayerFailed;

    private int currentScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void AddScore(int points)
    {
        currentScore += points;
        OnScoreUpdated?.Invoke(currentScore);
    }

    public void GenerateNewObstacleWord(string newWord)
    {
        OnTargetWordGenerated?.Invoke(newWord);
    }

    public void TriggerGameOver()
    {
        OnPlayerFailed?.Invoke();
    }
}