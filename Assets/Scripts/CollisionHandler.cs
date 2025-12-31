using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayLevelReloading = 2f;
    [SerializeField] AudioClip finishAudio;
    [SerializeField] AudioClip crashAudio;
    AudioSource ads;

    private void Start()
    {
        ads = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;

        switch (collider.tag)
        {
            case "Friendly":
                Debug.Log("You are friendly");
                break;
            case "Finish":
                PlayAudio(finishAudio);
                break;
            case "Fuel":
                Debug.Log("You hit fuel");
                break;
            default:
                PlayAudio(crashAudio);
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
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
        if (!ads.isPlaying)
        {
            ads.PlayOneShot(audioSrc);
        }
    }
}
