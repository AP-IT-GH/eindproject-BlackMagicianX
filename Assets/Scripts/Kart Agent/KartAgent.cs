using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class KartAgent : Agent
{
    public CheckpointManager _checkpointManager;
    private KartController _kartController;

    public override void Initialize()
    {
        _kartController = GetComponent<KartController>();
    }

    public override void OnEpisodeBegin()
    {
        _checkpointManager.ResetCheckpoints();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 diff = _checkpointManager.nextCheckPointToReach.transform.position - transform.position;
        sensor.AddObservation(diff / 20f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var Actions = actions.ContinuousActions;

        if (_kartController != null)
        {
            _kartController.ApplyAcceleration(Actions[1]);
            _kartController.Steer(Actions[0]);
            _kartController.AnimateKart(Actions[0]);

            // Penalize for moving backward
            Vector3 localVelocity = transform.InverseTransformDirection(_kartController.hitbox.velocity);
            if (localVelocity.z < 0)
            {
                AddReward(-0.01f);
            }
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.ContinuousActions;
        action.Clear();

        action[0] = Input.GetAxis("Horizontal"); // Steering
        action[1] = Input.GetKey(KeyCode.W) ? 1f : 0; // Gas or no Gas
    }
}
