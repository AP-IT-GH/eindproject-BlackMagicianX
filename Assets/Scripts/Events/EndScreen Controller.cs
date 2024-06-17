using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    public TextMeshPro placementText;

    void Start()
    {
        switch (ResultController.playerPlace)
        {
            case 1:
                placementText.text = $"Congrats {ResultController.playerName}! You placed {ResultController.playerPlace}st place.";
                break;
            case 2:
                placementText.text = $"Congrats {ResultController.playerName}! You placed {ResultController.playerPlace}nd place.";
                break;
            case 3:
                placementText.text = $"Congrats {ResultController.playerName}! You placed {ResultController.playerPlace}rd place.";
                break;
            default:
                placementText.text = $"Congrats {ResultController.playerName}! You placed {ResultController.playerPlace}th place.";
                break;
        }
    }
}
