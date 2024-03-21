using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public float maxSpawnPointX;
    public GameObject menuPanel;

    bool gameStarted = false;
    int score = 0;
    int highScore = 0;

    public Text scoreText;
    public Text highScoreText;

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    void Update()
    {
        if (Input.anyKeyDown && !gameStarted)
        {
            menuPanel.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            StartCoroutine("SpawnEnemies");
            gameStarted = true;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            Spawn();
        }
    }

    public void Spawn()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs are assigned.");
            return;
        }

        int randomPrefabIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemyPrefab = enemyPrefabs[randomPrefabIndex];

        float randomSpawnX = Random.Range(-maxSpawnPointX, maxSpawnPointX);
        Vector3 enemySpawnPos = spawnPoint.position;
        enemySpawnPos.x = randomSpawnX;

        Instantiate(selectedEnemyPrefab, enemySpawnPos, Quaternion.identity);
    }

    public void Restart()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        SceneManager.LoadScene(0);
    }

    public void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
