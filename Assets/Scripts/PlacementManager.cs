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

    // Start is called before the first frame update
    void Start()
    {
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
        }

        foreach (var player in playerList)
        {
            if (currentLap < player.GetComponent<RacingManager>().GetCurrentLap() && currentLap == 2)
            {
                //resultList.Add();
            }
        }

        if (currentLap == 3)
        {
            Debug.Log(resultList);
        }
    }
}
