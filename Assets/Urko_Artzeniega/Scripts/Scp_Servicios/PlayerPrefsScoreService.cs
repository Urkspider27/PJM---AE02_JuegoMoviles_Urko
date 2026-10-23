using UnityEngine;

public class PlayerPrefsScoreService : MonoBehaviour, IScoreService
{
    public void SaveHighScore(int score)
    {
        if (score > GetHighScore())
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}