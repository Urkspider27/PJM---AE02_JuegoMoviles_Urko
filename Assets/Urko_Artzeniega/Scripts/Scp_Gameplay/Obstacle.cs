using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 4f;
    private string assignedWord;
    private bool hasTriggeredSlowMo = false;
    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    public void SetWord(string word)
    {
        assignedWord = word;
    }

    void Update()
    {
        // Mueve el pincho hacia la izda constantemente
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (playerTransform != null)
        {
            // Calcula distancia entre pincho y jugador
            float distanciaX = transform.position.x - playerTransform.position.x;
            float distanciaY = Mathf.Abs(transform.position.y - playerTransform.position.y);

            // Muere al contacto con el pinmcho
            if (distanciaX <= 0.35f && distanciaX >= -0.1f && distanciaY < 0.5f)
            {
                Time.timeScale = 1f;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.TriggerGameOver();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Activa la cámara lenta al llegar a la zona invisible
        if (collision.gameObject.name == "SlowMoZone" && !hasTriggeredSlowMo)
        {
            hasTriggeredSlowMo = true;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GenerateNewObstacleWord(assignedWord);
            }
            Time.timeScale = 0.2f;
        }
    }
}