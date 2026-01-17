using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayLevelReloading = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] int maxLives;
    [SerializeField] TextMeshProUGUI livesText;

    AudioSource ads;
    static int currentLives;
    bool isControllable = true;
    public static bool isCollidable = true;

    private void Start()
    {
        ads = GetComponent<AudioSource>();
        if (currentLives <= 0)
        {
            currentLives = maxLives;
        }
        UpdateLivesUI();
    }

    private void Update()
    {
        //RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    private static void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;
        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable) { return; }
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
        currentLives--;
        UpdateLivesUI();
        isControllable = false;
        crashParticles.Play();
        PlayAudio(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayLevelReloading);
    }

    private void ReloadLevel()
    {
        if (currentLives > 0)
        {
            string currentScene = SceneManager.GetActiveScene().name; //Or can use buildIndex to get the index number of the scene
            SceneManager.LoadScene(currentScene);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void PlayAudio(AudioClip audioSrc)
    {
        ads.Stop();
        ads.PlayOneShot(audioSrc);

    }

    private void UpdateLivesUI()
    {
        livesText.text = currentLives.ToString();
    }
}
