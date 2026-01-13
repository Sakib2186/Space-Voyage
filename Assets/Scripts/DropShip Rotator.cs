using UnityEngine;

public class DropShipRotator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float rotateSpeed;
    private void FixedUpdate()
    {
        Rotator();
    }

    private void Rotator()
    {
        transform.Rotate(0f, 1f * Time.fixedDeltaTime * rotateSpeed, 0f);
    }
}
