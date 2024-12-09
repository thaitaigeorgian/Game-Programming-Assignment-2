using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject asteroidPrefab;
    public float spawnInterval = 1f;
    public float spawnRangeX = 10f;
    public float spawnRangeY = 5f;
    public float asteroidSpeed = 2f;
    public float timer;

    public GameObject pauseMenu;
    private bool isGamePaused = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        float x = Random.Range(-spawnRangeX, spawnRangeX);
        float y = Random.Range(-spawnRangeY, spawnRangeY);

        Vector2 spawnPosition = new Vector2();
        Vector2 direction = new Vector2();

        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // Trên
                spawnPosition = new Vector2(x, spawnRangeY);
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
            case 1: // Dưới
                spawnPosition = new Vector2(x, -spawnRangeY);
                direction = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 2: // Trái
                spawnPosition = new Vector2(-spawnRangeX, y);
                direction = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 3: // Phải
                spawnPosition = new Vector2(spawnRangeX, y);
                direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
        }

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * asteroidSpeed;
        }

        Destroy(asteroid, 20f);
    }

    public void TogglePauseMenu()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }



    public void GoToPlayScene()
    {

        ChangeScene("GameScene");
    }
    public void GoToTitleScene()
    {
        Time.timeScale = 1f;
        ChangeScene("TitleScene");
    }


    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
