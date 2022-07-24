using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    Movement movement;
    AudioSource myAudioSource;

    bool isTransitioning = false;

    void Awake() 
    {
        movement = GetComponent<Movement>();  
        myAudioSource = GetComponent<AudioSource>(); 
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) {return;}
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
    
            case "Finish":
                StartSuccessSequence();
                break;
    
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        movement.enabled = false;
        successParticles.Play();
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(successAudio);
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        movement.enabled = false;
        crashParticles.Play();
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(crashAudio);
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
 
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
