using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public enum FishType
{
    None = -1,
    Force = 0,
    Deflector = 1,
    Lightning = 2,
    Anchor = 3,
    Bubble = 4
}


public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] int startingTime, startingTimeTillBigOne;
    Coroutine fishingTimer;
    float timeRemaining, timeTillBigOne;
    int[] fishCollected = new int[] { 0, 0, 0, 0 };


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        StartFishing();
    }

    /// <summary>
    /// Start the timer and fish!
    /// </summary>
    public void StartFishing()
    {
        timeRemaining = startingTime;
        timeTillBigOne  = startingTimeTillBigOne;
        fishingTimer = StartCoroutine(FishingTimer());
    }

    /// <summary>
    /// Stop fishing
    /// Convert collected fish to spells
    /// </summary>
    public void EndFishing()
    {
        print("Game is over!!");
    }

    IEnumerator FishingTimer()
    {
        while (timeRemaining > 0)
        {
            yield return null;
            timeRemaining -= Time.deltaTime;
            timerText.text = ((int)timeRemaining).ToString();

            if (timeTillBigOne <= startingTimeTillBigOne)
            {
                if (timeTillBigOne < 0)
                {
                    SpawnBigOne();
                    timeTillBigOne = startingTimeTillBigOne + 1;
                    continue;
                }
                timeTillBigOne -= Time.deltaTime;
            }

        }
        EndFishing();
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public void AddTime(float amount)
    {
        timeRemaining += amount;
        if (timeRemaining <= 0) EndFishing();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fishType">What number means which fish is still up in the air</param>
    public void CollectFish(FishType fishType)
    {
        if ((int)fishType >= fishCollected.Length)
        {
            Debug.LogWarning("HEY!!! THE FISH COLLECTED ARRAY ISN'T BIG ENOUGH FOR THE FISH OF TYPE " +  fishType);
            return;
        }
        if (fishType == FishType.None) return;

        fishCollected[(int)fishType]++;
    }

    void SpawnBigOne()
    {
        print("Spawning The Big One");
    }
}
