using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{

    [SerializeField] InputAction thrust;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        thrust.Enable();
    }

    private void Update()
    {
        if (Time.time > 3.0f)
        {
            gameObject.SetActive(true);
        }
        if (thrust.IsPressed())
        {
            Debug.Log("Thrust has been pressed");
        }
    }

}
