using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{

    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStength;
    [SerializeField] float rotationStength;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem rocketThrust;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;
    AudioSource ads;
    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ads = GetComponent<AudioSource>();
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
            if(!rocketThrust.isPlaying)
            {
                rocketThrust.Play();
            }
            playAudio();
        }
        else
        {
            ads.Stop();
            rocketThrust.Stop();
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
                if (!rightThrust.isPlaying)
                {
                    leftThrust.Stop();
                    rightThrust.Play();
                }

            }
            else if(rotationInput > 0)
            {
                ApplyRotation(-rotationStength);
                if (!leftThrust.isPlaying)
                {
                    rightThrust.Stop();
                    leftThrust.Play();
                }

            }
            else
            {
                rightThrust.Stop();
                leftThrust.Stop();
            }
        }
        else
        {
            rightThrust.Stop();
            leftThrust.Stop();
        }

    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //not allowing physic system to rotate. We will rotate it
        transform.Rotate(0f, 0f, 1f * rotationThisFrame * Time.fixedDeltaTime); // OR Vector3.forward
        rb.freezeRotation = false;
        
    }

    private void playAudio()
    {
        if (!ads.isPlaying)
        {
            ads.PlayOneShot(mainEngineSFX);
        }
    }

}
