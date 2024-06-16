using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStartEvent : MonoBehaviour
{
    public GameObject startLightC;
    public GameObject startLightB;
    public GameObject startLightA;
    public Material redLightMaterial;
    public Material greenLightMaterial;

    void Start()
    {
        // Disable all lights at the start
        SetLightState(startLightA, false);
        SetLightState(startLightB, false);
        SetLightState(startLightC, false);

        // Start the countdown
        StartCoroutine(Countdown());
    }

    void SetLightState(GameObject lightObject, bool state)
    {
        lightObject.GetComponent<Renderer>().enabled = state;
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        SetLightState(startLightC, true);

        yield return new WaitForSeconds(1);
        SetLightState(startLightB, true);

        yield return new WaitForSeconds(1);
        SetLightState(startLightA, true);

        yield return new WaitForSeconds(1);
        TurnAllLightsGreen();
    }

    void TurnAllLightsGreen()
    {
        startLightA.GetComponent<Renderer>().material = greenLightMaterial;
        startLightB.GetComponent<Renderer>().material = greenLightMaterial;
        startLightC.GetComponent<Renderer>().material = greenLightMaterial;
    }
}
