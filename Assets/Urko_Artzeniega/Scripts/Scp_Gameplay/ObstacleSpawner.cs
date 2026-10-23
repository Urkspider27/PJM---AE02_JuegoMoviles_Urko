using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform spawnPoint;
    public float moveSpeed = 5f;

    private string[] wordList = { "UNIVERSIDAD", "ECLIPSE", "ORDENADOR", "RELAMPAGO", "VIDEOJUEGO", "PALABRA", "SALTO", "EUNEIZ" };

    public void SpawnObstacle()
    {
        string chosenWord = wordList[Random.Range(0, wordList.Length)];

        // Instanciamos el obstáculo
        // GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);

        // Le pasamos la palabra al gestor del juego para que la muestre arriba
        GameManager.Instance.GenerateNewObstacleWord(chosenWord);
    }
}