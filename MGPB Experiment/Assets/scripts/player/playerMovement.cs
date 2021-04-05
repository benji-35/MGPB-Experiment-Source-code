using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 15.0F;
    [SerializeField]
    private float runSpeed = 25.0F;
    private float speed;
    [SerializeField]
    private float jumpSpeed = 8.0F;
    [SerializeField]
    private float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;
    [SerializeField]
    private float sensitivity = 5;
    [SerializeField]
    private bool player1 = true;
    [SerializeField]
    private AudioClip walkSound;
    [SerializeField]
    private AudioClip runSound;
    [SerializeField]
    private AudioClip jumpSound;



    private AudioSource source;



    private string hor = "Horizontal";
    private string vert = "Vertical";
    private string mouseX = "Mouse X";
    private string mouseY = "Mouse Y";
    private string jump = "Jump";
    // Use this for initialization
    void Start()
    {
        speed = walkSpeed;
        source = GetComponent<AudioSource>();
        if (!player1)
        {
            hor = "Horizontal1";
            vert = "Vertical1";
            mouseX = "Mouse X1";
            mouseY = "Mouse Y1";
            jump = "Jump1";
        }
    }



    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            speed = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
                speed = runSpeed;
            moveDirection = new Vector3(Input.GetAxis(hor), 0, Input.GetAxis(vert));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton(jump))
            {
                if (source != null && jumpSound != null)
                    source.PlayOneShot(jumpSound);
                moveDirection.y = jumpSpeed;
            }
            else
            {
                if (moveDirection.x != 0 || moveDirection.z != 0)
                {
                    if (source != null && jumpSound != null)
                    {
                        if (speed == runSpeed)
                        {
                            source.PlayOneShot(runSound);
                        }
                        else
                        {
                            source.PlayOneShot(walkSound);
                        }
                    }
                }
            }
        }
        turner = Input.GetAxis(mouseX) * sensitivity;
        looker = -Input.GetAxis(mouseY) * sensitivity;
        if (turner != 0)
        {
            transform.eulerAngles += new Vector3(0, turner, 0);
        }
        if (looker != 0)
        {
            transform.eulerAngles += new Vector3(looker, 0, 0);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }



    public bool getPlayer()
    {
        return player1;
    }
}