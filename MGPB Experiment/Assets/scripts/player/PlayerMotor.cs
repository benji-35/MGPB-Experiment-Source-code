using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 camRotation;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        performMovement();
        performRotation();
    }

    private void performMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        }
    }

    private void performRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        cam.transform.Rotate(-camRotation);
    }

    public void Move(Vector3 velo)
    {
        this.velocity = velo;
    }

    public void Rotate(Vector3 rot)
    {
        this.rotation = rot;
    }

    public void RotateCamera(Vector3 rot)
    {
        this.camRotation = rot;
    }
}
