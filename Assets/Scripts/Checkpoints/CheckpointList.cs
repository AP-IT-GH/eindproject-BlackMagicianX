using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointList : MonoBehaviour
{
    public List<Checkpoint> checkpointList;

    private void Awake()
    {
        checkpointList = new List<Checkpoint>(GetComponentsInChildren<Checkpoint>());
    }
}
