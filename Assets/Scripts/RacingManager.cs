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
    }

    void Update()
    {
        if (lap == 3)
        {
            NextScene();
        }

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
            Debug.Log($"Current lap: {lap}");
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

    public void NextScene()
    {
        Debug.Log("Next scene");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
