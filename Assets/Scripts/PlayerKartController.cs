using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKartController : MonoBehaviour
{
    public InputActionReference Accelerate;
    public InputActionReference Brake;
    public Transform kartModel;
    public Transform kartNormal;
    public Rigidbody hitbox;

    public float speed, currentSpeed;
    public float rotate, currentRotate;
    public float accelerationInput = 0f;
    public float brakeInput = 0f;

    [Header("Parameters")]
    public float acceleration = 30f;
    public float braking = 50f;
    public float steering = 80f;
    public float gravity = 10f;
    public LayerMask layerMask;

    [Header("Model Parts")]
    public Transform frontWheels;
    public Transform backWheels;
    public Transform steeringWheelModel; // Changed to differentiate from the SteeringWheel component

    public int laps = 0;

    private void Awake()
    {
        Accelerate.action.Enable();
        Brake.action.Enable();

        Accelerate.action.performed += ctx => accelerationInput = ctx.ReadValue<float>();
        Accelerate.action.canceled += ctx => accelerationInput = 0f;
        Brake.action.performed += ctx => brakeInput = ctx.ReadValue<float>();
        Brake.action.canceled += ctx => brakeInput = 0f;
    }

    private void OnDestroy()
    {
        Accelerate.action.Disable();
        Brake.action.Disable();
    }

    private void Update()
    {
        ApplyAcceleration(accelerationInput);
        ApplyBraking(brakeInput);

        steeringWheelModel.localPosition = new Vector3(-0.2956806f, 0.4435726f, 0.05468971f);
    }

    public void ApplyAcceleration(float input)
    {
        speed = acceleration * input;
        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f);
    }

    public void ApplyBraking(float input)
    {
        speed = -braking * input;
        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f);
    }

    private void FixedUpdate()
    {
        hitbox.AddForce(-kartModel.transform.right * currentSpeed, ForceMode.Acceleration);

        // Gravity
        hitbox.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        // Follow Collider
        transform.position = hitbox.transform.position - new Vector3(0, 0.4f, 0);

        // Steering
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);

        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out RaycastHit hitOn, 1.1f, layerMask);
        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out RaycastHit hitNear, 2.0f, layerMask);

        // Normal Rotation
        kartNormal.up = Vector3.Lerp(kartNormal.up, hitNear.normal, Time.deltaTime * 8.0f);
        kartNormal.Rotate(0, transform.eulerAngles.y, 0);
    }

    public void Steer(float steeringSignal)
    {
        int steerDirection = steeringSignal > 0 ? 1 : -1;
        float steeringStrength = Mathf.Abs(steeringSignal);

        rotate = (steering * steerDirection) * steeringStrength;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f);

        AnimateKart(steeringSignal);
    }

    public void SetSteeringInput(float input)
    {
        Steer(input / 45f); // Normalize the input range if needed
    }

    public void AnimateKart(float input)
    {
        kartModel.localEulerAngles = Vector3.Lerp(kartModel.localEulerAngles, new Vector3(0, 90 + (input * 15), kartModel.localEulerAngles.z), .2f);

        frontWheels.localEulerAngles = new Vector3(0, (input * 15), frontWheels.localEulerAngles.z);
        frontWheels.localEulerAngles += new Vector3(0, 0, hitbox.velocity.magnitude / 2);
        backWheels.localEulerAngles += new Vector3(0, 0, hitbox.velocity.magnitude / 2);

        steeringWheelModel.localEulerAngles = new Vector3(-25, 90, ((input * 45)));
    }
}
