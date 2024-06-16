using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public List<GameObject> playerList;
    private List<String> resultList;

    private int currentLap;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        resultList = new List<String>();
        currentLap = Int32.MinValue;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var player in playerList)
        {
            if (currentLap < player.GetComponent<RacingManager>().GetCurrentLap())
            {
                currentLap = player.GetComponent<RacingManager>().GetCurrentLap();
                Debug.Log($"Current lap: {currentLap}");
            }

            if (player.GetComponent<RacingManager>().GetCurrentLap() == 4 && !resultList.Contains(player.GetComponent<RacingManager>().GetRacerName()))
            {
                resultList.Add(player.GetComponent<RacingManager>().GetRacerName());
            }
        }

        if (currentLap == 4)
        {
            for (int i = 0; i < resultList.Count; i++)
            {
                Debug.Log($"{i + 1}: {resultList[i]}");
            }
        }
    }
}
