using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class door : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    public bool canOpen;
    [SerializeField]
    private Material lockColor;
    [SerializeField]
    Material unlockColor;
    [SerializeField]
    private GameObject LightStatus;
    [SerializeField]
    private NetworkAnimator animNet;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animNet = GetComponent<NetworkAnimator>();
        if (animator == null)
        {
            Debug.LogError("Error: no animator in door object");
            GetComponent<door>().enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canOpen)
        {
            LightStatus.GetComponent<Renderer>().material = unlockColor;
        }
        else
        {
            LightStatus.GetComponent<Renderer>().material = lockColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canOpen && animNet != null)
        {
            animNet.ResetTrigger("Close");
            animNet.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && canOpen && animNet != null)
        {
            animNet.ResetTrigger("Open");
            animNet.SetTrigger("Close");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && canOpen && animNet != null)
        {
            animNet.ResetTrigger("Close");
            animNet.SetTrigger("Open");
        }
    }

    public Material unlockMat()
    {
        return (this.unlockColor);
    }

    public Material lockMat()
    {
        return (this.lockColor);
    }

    public void changeColorDoor(bool b)
    {
        canOpen = b;
        if (!b)
        {
            LightStatus.GetComponent<Renderer>().material = unlockColor;
        } else
        {
            LightStatus.GetComponent<Renderer>().material = lockColor;
        }
    }
}
