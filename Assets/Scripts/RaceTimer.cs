using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    private bool timerRunning = false;

    private float raceTime = 0f;
    private float penaltyTime = 1f;

    [SerializeField] private Leaderboard leaderboard;

    private void OnEnable()
    {
        GameEvents.raceStart += StartRace;
        GameEvents.raceEnd += EndRace;
        GameEvents.racePenalty += AddSeconds;
    }

    private void OnDisable()
    {
        GameEvents.raceStart -= StartRace;
        GameEvents.raceEnd -= EndRace;
        GameEvents.racePenalty -= AddSeconds;
    }

    private void Update()
    {
        if (timerRunning)
        {
            raceTime += Time.deltaTime; 
        }
    }

    private void StartRace()
    {
        timerRunning = true; 
        raceTime = 0f; 
        Debug.Log("Race started!");
    }

    private void AddSeconds()
    {
        Debug.Log(raceTime);
        raceTime = raceTime + penaltyTime;
        Debug.Log("You have been penalized!");
        Debug.Log(raceTime);

    }

    private void EndRace()
    {
        timerRunning = false;
        GameData.Instance.racesCompleted++;
        Debug.Log("Race ended! Total time: " + raceTime + " seconds");
        Debug.Log("Races completed: " + GameData.Instance.racesCompleted);
        leaderboard.AddTime(raceTime);
    }
}
