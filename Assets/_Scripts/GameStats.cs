using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    private static GameStats instance;
    public static GameStats Instance { get { return instance; } }

    //Score
    public float score;
    public float highScore;
    public float distanceModifier = 1.5f;

    //Collectables
    public int totalCollectables;
    public int collectedThisRound;
    public float pointsCollectable = 10.0f;

    //Internal Cooldown
    private float lastScoreUpdate;
    private float scoreUpdateDelta = 0.2f;

    //Action
    public Action<int> OnCollectableChange;
    public Action<float> OnScoreChange;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        float s = GameManager.Instance.motor.transform.position.z * distanceModifier;
        s += collectedThisRound * pointsCollectable;

        if (s > score)
        {
            score = s;
            if(Time.time - lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time;
                OnScoreChange?.Invoke(score);
            }
        }
    }

    public void CollectCollectable()
    {
        collectedThisRound++;
        OnCollectableChange?.Invoke(collectedThisRound);
    }

    public void ResetSession()
    {
        score = 0;
        collectedThisRound = 0;

        OnScoreChange?.Invoke(score);
        OnCollectableChange?.Invoke(collectedThisRound);
    }

    public string ScoreToText()
    {
        return ((int)score).ToString("0000000");
    }

    public string CollectablesToText()
    {
        return "x" + collectedThisRound.ToString("000");
    }
}
