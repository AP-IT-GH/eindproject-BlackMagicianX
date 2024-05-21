using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RacingManager : MonoBehaviour
{
    private bool lapComplete;
    private int lap;

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
            Debug.Log($"Current lap: {lap}");
        }
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
