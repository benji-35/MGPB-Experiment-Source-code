using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public float runSpeed = 15f;
    public float gravity = -9.81f;
    public float jumpSpeed = 1F;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    float currSpeed;

    // Update is called once per frame
    void Update()
    {
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        currSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
            currSpeed = runSpeed;
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y += jumpSpeed;
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}