using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public List<GameObject> playerList;
    private List<String> resultList;

    private RacingManager racingManager;
    private bool playerFinished;
    private string playerName;

    private int currentLap;
    private int laps;

    // Start is called before the first frame update
    void Start()
    {
        racingManager = new RacingManager();
        resultList = new List<String>();
        currentLap = Int32.MinValue;
        playerFinished = false;
        laps = 2;
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

            if (player.GetComponent<RacingManager>().GetCurrentLap() == laps && !resultList.Contains(player.GetComponent<RacingManager>().GetRacerName()))
            {
                if (player.CompareTag("Player"))
                {
                    playerFinished = true;
                    playerName = player.GetComponent<RacingManager>().GetRacerName();
                }
                resultList.Add(player.GetComponent<RacingManager>().GetRacerName());
            }
        }

        if (currentLap == laps && playerFinished)
        {
            ResultController.playerName = this.playerName;
            
            for (int i = 0; i < resultList.Count; i++)
            {
                if (resultList[i] == playerName)
                    ResultController.playerPlace = i + 1;
            }

            racingManager.NextScene();
        }
    }
}
