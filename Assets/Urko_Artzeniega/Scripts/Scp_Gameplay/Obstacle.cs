using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 4f;
    private string assignedWord;
    private bool hasTriggered = false;

    public void SetWord(string word)
    {
        assignedWord = word;
    }

    void Update()
    {
        // Mueve el obstáculo hacia la izquierda constantemente
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta la zona de cámara lenta por nombre de obj
        if (collision.gameObject.name == "SlowMoZone" && !hasTriggered)
        {
            hasTriggered = true;
            GameManager.Instance.GenerateNewObstacleWord(assignedWord);
            Time.timeScale = 0.2f; // Activa cam lenta
        }
        // Detecta si choca con el jugador
        else if (collision.CompareTag("Player"))
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
}