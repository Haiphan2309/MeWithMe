using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    const int NUM_LEVEL = 9;
    public static int currentLevel = 0;
    static public bool[] isCompleteLevel = new bool[NUM_LEVEL];
    static public bool[] isGetFairy = new bool[NUM_LEVEL];
    
}
