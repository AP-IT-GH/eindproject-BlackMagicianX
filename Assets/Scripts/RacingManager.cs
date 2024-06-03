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

    public string RacerName;

    // Start is called before the first frame update
    void Start()
    {
        lapComplete = true;
        lap = 0;
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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            kartAgent.AddReward(-0.5f); // Adjust the penalty value as needed
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
