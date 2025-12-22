using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
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
                break;
        }
    }
}
