using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    public int lastLevelUnlocked;
    public int[] starsEarnedPerLevel;

    public int[] scoresPerLevel;

    public Player()
    {
        lastLevelUnlocked = 1;
        starsEarnedPerLevel = new int[20];
        scoresPerLevel = new int[20];
    }

    public void SetLastLevelUnlocked(int level)
    {
        lastLevelUnlocked = level;
    }

    public void SetStarsEarned(int level, int starsEarned)
    {
        starsEarnedPerLevel[level] = starsEarned;
    }
}
