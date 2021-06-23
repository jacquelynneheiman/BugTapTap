using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public const int NUM_LEVELS = 20;

    public Player player;
    public PlayerData playerData;
    public bool[] levelsPlayed;

    private int levelToLoad;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        levelsPlayed = new bool[20];

        for (int i = 0; i < NUM_LEVELS; i++)
        {
            levelsPlayed[i] = false;
        }

        player = GetComponent<Player>();
        playerData = new PlayerData(player);

        LoadData();
    }

    private void Start()
    {
        
    }

    public int LevelToLoad
    {
        get
        {
            return levelToLoad;
        }

        set
        {
            levelToLoad = value;
        }
    }

    public void PlayedLevel()
    {
        levelsPlayed[levelToLoad - 1] = true;

        if(levelToLoad + 1 > player.lastLevelUnlocked)
        {
            player.lastLevelUnlocked = levelToLoad + 1;
        }
    }

    void LoadData()
    {
        playerData = SaveSystem.LoadPlayer();

        if(playerData != null)
        {
            player.lastLevelUnlocked = playerData.lastLevelUnlocked;
            player.starsEarnedPerLevel = playerData.starsEarnedPerLevel;
            player.scoresPerLevel = playerData.scoresPerLevel;
        }
        
    }
}
