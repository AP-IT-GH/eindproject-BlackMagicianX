using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public float MaxTimeToReachNextCheckpoint = 30f;
    public float TimeLeft = 30f;

    public KartAgent kartAgent;
    public Checkpoint nextCheckPointToReach;
    
    private int CurrentCheckpointIndex;
    private List<Checkpoint> CheckpointList;
    private Checkpoint lastCheckpoint;

    public event Action<Checkpoint> reachedCheckpoint;

    public RacingManager racingManager;

    void Start()
    {
        CheckpointList = FindObjectOfType<CheckpointList>().checkpointList;
        ResetCheckpoints();
    }

    public void ResetCheckpoints()
    {
        CurrentCheckpointIndex = 0;
        TimeLeft = MaxTimeToReachNextCheckpoint;
        
        SetNextCheckpoint();
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;

        if (TimeLeft < 0f)
        {
            kartAgent.AddReward(-1f);
            kartAgent.EndEpisode();
        }
    }

    public void CheckPointReached(Checkpoint checkpoint)
    {
        Debug.Log($"{CurrentCheckpointIndex}");
        if (nextCheckPointToReach != checkpoint)
        {
            // Penalize for passing the wrong checkpoint
            kartAgent.AddReward(-0.5f);
            return;
        }

        lastCheckpoint = CheckpointList[CurrentCheckpointIndex];
        reachedCheckpoint?.Invoke(checkpoint);
        CurrentCheckpointIndex++;

        if (CurrentCheckpointIndex >= CheckpointList.Count)
        {
            racingManager.LapComplete();
            kartAgent.AddReward(0.5f);
            kartAgent.EndEpisode();
        }
        else if(nextCheckPointToReach == checkpoint)
        {
            kartAgent.AddReward(0.25f + 0.005f * CurrentCheckpointIndex);
            Debug.Log($"Gained {0.25f + 0.005f * CurrentCheckpointIndex}");
            SetNextCheckpoint();
        }
    }

    private void SetNextCheckpoint()
    {
        if (CheckpointList.Count > 0)
        {
            TimeLeft = MaxTimeToReachNextCheckpoint;
            nextCheckPointToReach = CheckpointList[CurrentCheckpointIndex];
        }
    }
}
