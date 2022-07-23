using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody myRigidbody;
    AudioSource myAudioSource;
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float rotationThrust = 1f;
    void Awake() 
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustSpeed = mainThrust * Time.deltaTime;
            myRigidbody.AddRelativeForce(Vector3.up * thrustSpeed);
            if (!myAudioSource.isPlaying)
            {
                myAudioSource.Play();
            }
        }
        else
        {
            myAudioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        myRigidbody.freezeRotation = true;
        float rotationSpeed = rotationThisFrame * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationSpeed);
        myRigidbody.freezeRotation = false;
    }
}
