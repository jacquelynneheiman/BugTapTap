using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [Header("Level Info")]
    public int levelNumber;
    public int waveLength;

    [Header("Spawnning")]
    public List<BugType> bugTypesInLevel;
    public Transform[] spawnLocations;
    public float spawnInterval;
    public GameObject[] bugPrefabs;

    [Header("Scoring")]
    public int correct;
    public int incorrect;
    public int score;
    public Text scoreText;

    [Header("Victory UI")]
    public GameObject scoreUI;
    public Image[] stars;
    public GameObject highScoreText;
    public Text finalScoreText;
    public Sprite starEnabled;
    public Sprite starDisabled;

    [Header("Game Over UI")]
    public GameObject gameOverUI;
    public Text gameOverScoreText;

    [Header("Pause Menu UI")]
    public GameObject pauseMenu;

    [Header("Lives")]
    public int health;
    public GameObject[] hearts;


    [HideInInspector] public int bugsSpawned;

    private float spawnTimer;
    List<BugType> spawnList;
    

    private void Start()
    { 
        spawnList = new List<BugType>();

        for(int i = 0; i < waveLength; i++)
        {
            int randType = Random.Range(0, bugTypesInLevel.Count);
            BugType bugType = bugTypesInLevel[randType];

            spawnList.Add(bugType);
        }
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        
        if(spawnTimer <= 0)
        {
            SpawnBug();
            spawnTimer = spawnInterval;
        }

        scoreText.text = score.ToString();

        if(correct + incorrect == waveLength)
        {
            if(incorrect < 3)
            {
                ShowScore();
            }
        }

    }

    public void LoseHealth()
    {
        health--;

        switch(health)
        {
            case 3:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                break;
            case 2:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(false);
                break;
            case 1:
                hearts[0].SetActive(true);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                break;
            case 0:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                GameOver();
                break;
        }
    }

    void SpawnBug()
    {
        if (spawnList.Count > 0)
        {
            int randomSpawn = Random.Range(0, 5);

            GameObject newBug = Instantiate(bugPrefabs[(int)spawnList[0]], spawnLocations[randomSpawn].position, Quaternion.identity);
            newBug.GetComponent<Bug>().level = this;
            spawnList.RemoveAt(0);
            bugsSpawned++; 
        }
    }

    void ShowScore()
    {
        scoreUI.SetActive(true);

        float starScore = (float)health / 3;

        Debug.Log("Star Score: " + starScore);

        if(starScore >= .666f)
        {
            //three stars
            stars[0].sprite = starEnabled;
            stars[1].sprite = starEnabled;
            stars[2].sprite = starEnabled;

            if (GameManager.instance.player.starsEarnedPerLevel[levelNumber - 1] < 3)
            {
                GameManager.instance.player.starsEarnedPerLevel[levelNumber - 1] = 3; 
            }
        }
        else if(starScore >= .333f && starScore < .666f)
        {
            //two stars
            stars[0].sprite = starEnabled;
            stars[1].sprite = starEnabled;
            stars[2].sprite = starDisabled;

            if (GameManager.instance.player.starsEarnedPerLevel[levelNumber - 1] < 2)
            {
                GameManager.instance.player.starsEarnedPerLevel[levelNumber - 1] = 2;
            }
        }
        else if(starScore >= .333f && starScore < .666f)
        {
            //one star
            stars[0].sprite = starEnabled;
            stars[1].sprite = starDisabled;
            stars[2].sprite = starDisabled;

            if (GameManager.instance.player.starsEarnedPerLevel[levelNumber - 1] < 1)
            {
                GameManager.instance.player.starsEarnedPerLevel[levelNumber - 1] = 1;
            }
        }

        finalScoreText.text = score.ToString();

        if(score > GameManager.instance.player.scoresPerLevel[levelNumber - 1])
        {
            highScoreText.SetActive(true);
            GameManager.instance.player.scoresPerLevel[levelNumber - 1] = score;
        }

        SaveSystem.SavePlayer(GameManager.instance.player);
        
    }

    void GameOver()
    {
        scoreUI.SetActive(false);

        Time.timeScale = 0;
        gameOverUI.SetActive(true);
        gameOverScoreText.text = score.ToString();

    }

    public void RetryLevel()
    {
        Time.timeScale = 1;
        GameManager.instance.LevelToLoad = levelNumber;
        SceneManager.LoadScene("Loading");
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        GameManager.instance.LevelToLoad = levelNumber + 1;
        SceneManager.LoadScene("Loading");
    }

    public void ExitLevel()
    {
        Time.timeScale = 1;
        GameManager.instance.LevelToLoad = -1;
        SceneManager.LoadScene("Loading");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}

public enum BugType
{
    bad1, bad2, bad3, blink, zigzag, invisible, invisible_A, bad1_A, bad2_A, bad3_A,  blink_A,  zigzag_A, good
}
