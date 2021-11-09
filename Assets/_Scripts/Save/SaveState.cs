using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    public int Highscore { set; get; }
    public int TotalCollectables { set; get; }
    public DateTime LastSaveTime;

    public SaveState()
    {
        Highscore = 0;
        TotalCollectables = 0;
        LastSaveTime = DateTime.Now;
    }
}
