using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGame : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Quit is pressed");
            Application.Quit();
        }
        
    }
}
