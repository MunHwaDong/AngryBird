using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[System.Serializable]
public class PlayData
{
    public int currentScore = 0;
    public int highScore = 0;
    
    public int currentEnemiesNum = 0;

    public PlayData()
    {
        currentScore = 0;
        highScore = 0;
        currentEnemiesNum = 0;
    }

    public void UpdateCurrentScore(int score)
    {
        currentScore += score;
    }
}
