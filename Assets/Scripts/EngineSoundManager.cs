using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public float minPitch = 1.0f;
    public GameObject kart;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minPitch;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.pitch = minPitch + 0.02f * kart.GetComponent<KartController>().currentSpeed;
    }
}
