using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public const int NUM_LEVEL = 9;
    public static int currentLevel = 0;
    static public bool[] isCompleteLevel = new bool[NUM_LEVEL];
    static public bool[] isGetFairy = new bool[NUM_LEVEL];

    public int currentLevelData;
    public bool[] isCompleteLevelData = new bool[NUM_LEVEL];
    public bool[] isGetFairyData = new bool[NUM_LEVEL];

    public GameData()
    {
        currentLevelData = currentLevel;
        for (int i = 0; i < NUM_LEVEL; i++)
        {
            isCompleteLevelData[i] = isCompleteLevel[i];
            isGetFairyData[i] = isGetFairy[i];
        }
    }

    static public int numOfFairy()
    {
        int count = 0;
        for (int i = 0; i < NUM_LEVEL; i++)
        {
            if (isGetFairy[i] == true) count++;
        }
        return count;
    }
}
