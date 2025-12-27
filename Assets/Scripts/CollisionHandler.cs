using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayLevelReloading = 2f;

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
                Debug.Log("You Finished");
                break;
            case "Fuel":
                Debug.Log("You hit fuel");
                break;
            default:
                Debug.Log("You exploded");
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
}
