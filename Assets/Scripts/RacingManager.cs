using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RacingManager : MonoBehaviour
{
    private bool lapComplete;
    private int lap;
    public KartAgent kartAgent;
    public KartController kartController;
    public string racerName;
    private float currentAccel;
    private bool isOffRoad;
    private bool isOnRoad;

    // Start is called before the first frame update
    void Start()
    {
        lapComplete = true;
        lap = 0;
        currentAccel = kartController.acceleration;
        isOffRoad = false;
        isOnRoad = false;

        kartController.acceleration = 0f;
        kartController.gravity = 0f;
        kartController.steering = 0f;
        StartCoroutine(Countdown(4));
    }

    void Update()
    {
        // Apply continuous penalty if the kart is off-road
        if (isOffRoad)
        {
            kartAgent.AddReward(-0.01f);
        }

        // Apply continuous reward if the kart is on the road
        if (isOnRoad)
        {
            kartAgent.AddReward(0.001f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Start" && lapComplete)
        {
            lapComplete = false;
            lap++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            kartAgent.AddReward(-1f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("OffRoad"))
        {
            isOffRoad = true;
            isOnRoad = false;
            kartController.acceleration = 30f;
        }
        else if (collision.gameObject.CompareTag("Road"))
        {
            isOffRoad = false;
            isOnRoad = true;
            kartController.acceleration = currentAccel;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("OffRoad"))
        {
            isOffRoad = false;
            kartController.acceleration = currentAccel;
        }
        else if (collision.gameObject.CompareTag("Road"))
        {
            isOnRoad = false;
        }
    }

    public int GetCurrentLap()
    {
        return lap;
    }

    public void LapComplete()
    {
        lapComplete = true;
    }

    public string GetRacerName() {  return racerName; }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {

            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        kartController.acceleration = currentAccel;
        kartController.gravity = 25f;
        kartController.steering = 15f;
    }
}