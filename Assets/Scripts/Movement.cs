using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip rocketThrust;
    [SerializeField] ParticleSystem rocketParticles;
    [SerializeField] ParticleSystem leftSideThrusterParticles;
    [SerializeField] ParticleSystem rightSideThrusterParticles;

    Rigidbody myRigidbody;
    AudioSource myAudioSource;
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {

            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {

            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        float thrustSpeed = mainThrust * Time.deltaTime;
        myRigidbody.AddRelativeForce(Vector3.up * thrustSpeed);
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(rocketThrust);
        }
        if (!rocketParticles.isPlaying)
        {
            rocketParticles.Play();
        }
    }

    void StopThrusting()
    {
        rocketParticles.Stop();
        myAudioSource.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightSideThrusterParticles.isPlaying)
        {
            rightSideThrusterParticles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftSideThrusterParticles.isPlaying)
        {
            leftSideThrusterParticles.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        myRigidbody.freezeRotation = true;
        float rotationSpeed = rotationThisFrame * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationSpeed);
        myRigidbody.freezeRotation = false;
    }

    void StopRotating()
    {
        rightSideThrusterParticles.Stop();
        leftSideThrusterParticles.Stop();
    }

}
