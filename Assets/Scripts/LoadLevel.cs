using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    [SerializeField] float delayLevelLoading = 2f;

    bool isControllable = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable) { return; }

        if (collision.gameObject.tag == "Finish")
        {
            //todo add sfx and particles
            StartSuccessSequence();
            
        }
    }

    private void StartSuccessSequence()
    {
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        Invoke("loadNextLevel", delayLevelLoading);
    }

    private void loadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneNumber = currentScene + 1;
        if (nextSceneNumber == SceneManager.sceneCountInBuildSettings) //This property returns the count of the number of scenes in my buildProfile.
        {
            nextSceneNumber = 0;
        }
        SceneManager.LoadScene(nextSceneNumber);
    }

}
