using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{

    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStength;
    [SerializeField] float rotationStength;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {

        ProcessThrusting();
        ProcessRotation();

    }

    private void ProcessThrusting()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStength * Time.fixedDeltaTime);
        }

    }
    private void ProcessRotation()
    {
        if (rotation.IsPressed())
        {
            float rotationInput = rotation.ReadValue<float>();
            if (rotationInput < 0)
            {
                ApplyRotation(rotationStength);
            }
            else
            {
                ApplyRotation(-rotationStength);
            }
            
        }

    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(0f, 0f, 1f * rotationThisFrame * Time.fixedDeltaTime); // OR Vector3.forward
    }


}
