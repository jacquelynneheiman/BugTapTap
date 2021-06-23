using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int lastLevelUnlocked;
    public int[] starsEarnedPerLevel;
    public int[] scoresPerLevel;

    public PlayerData(Player player)
    {
        lastLevelUnlocked = player.lastLevelUnlocked;
        starsEarnedPerLevel = player.starsEarnedPerLevel;
        scoresPerLevel = player.scoresPerLevel;
    }
}