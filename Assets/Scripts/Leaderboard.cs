using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<float> bestTimes = new();

    private void Awake()
    {
        bestTimes.Clear();
        for (int i = 0; i < 5; i++) 
        {
            bestTimes.Add(PlayerPrefs.GetFloat("time" + i, 9999999));
        }
    }

    public void AddTime(float time)
    {
        bestTimes.Add(time);
        bestTimes.Sort();
        SaveTimes();
    }

    private void SaveTimes()
    {
        for (int i= 0; i<5; i++)
        {
            if (i < bestTimes.Count)
            {
                PlayerPrefs.SetFloat("time" + i , bestTimes[i]);
            }
        }
        PlayerPrefs.Save();
    }



}
