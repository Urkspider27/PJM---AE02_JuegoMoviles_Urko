using UnityEngine;
using TMPro;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform spawnPoint;
    public float spawnRate = 4f;
    private float timer = 0f;

    private string[] wordList = { "UNIVERSIDAD", "ECLIPSE", "ORDENADOR", "EXPERTO", "VIDEOJUEGO", "PALABRA", "SALTO", "EUNEIZ" };

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        string chosenWord = wordList[Random.Range(0, wordList.Length)];
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
        obstacle.GetComponent<Obstacle>().SetWord(chosenWord);
    }
}