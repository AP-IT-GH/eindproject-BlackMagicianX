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

    // Start is called before the first frame update
    void Start()
    {
        lapComplete = true;
        lap = 0;
        currentAccel = kartController.acceleration;
    }

    void Update()
    {
        if (lap == 3)
        {
            NextScene();
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
            kartAgent.AddReward(-1f); // Adjust the penalty value as needed
            Debug.Log("bonk");
        }
        
        if (collision.gameObject.CompareTag("OffRoad"))
        {
            kartAgent.AddReward(-1f); // Adjust the penalty value as needed
            kartController.acceleration = 30f;
            Debug.Log("Off Road");
        }
        else if (collision.gameObject.CompareTag("Road"))
        {
            kartController.acceleration = currentAccel;
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
