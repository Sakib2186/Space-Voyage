using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayLevelReloading = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource ads;

    bool isControllable = true;

    private void Start()
    {
        ads = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKey();
    }

    private void RespondToDebugKey()
    {
        if (Keyboard.current.lKey.isPressed)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int nextLevel = currentLevel + 1;
            if (nextLevel == SceneManager.sceneCountInBuildSettings)
            {
                nextLevel = 0;
            }
            SceneManager.LoadScene(nextLevel);
            
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable) { return; }
        GameObject collider = collision.gameObject;
        switch (collider.tag)
        {
            case "Friendly":
                Debug.Log("You are friendly");
                break;
            case "Finish":
                PlayAudio(successSFX);
                break;
            case "Fuel":
                Debug.Log("You hit fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        crashParticles.Play();
        PlayAudio(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayLevelReloading);
    }

    private void ReloadLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name; //Or can use buildIndex to get the index number of the scene
        SceneManager.LoadScene(currentScene);
    }

    private void PlayAudio(AudioClip audioSrc)
    {
        ads.Stop();
        ads.PlayOneShot(audioSrc);

    }
}
