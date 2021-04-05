using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class notebloc : MonoBehaviour
{
    [SerializeField]
    private GameObject[] panel;
    private string save;
    [SerializeField]
    private Text[] notes;
    [SerializeField]
    private InputField[] enter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        playerMovement move = other.GetComponent<playerMovement>();
        if (move == null)
            return;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;
        playerMovement move = other.GetComponent<playerMovement>();
        if (move == null)
            return;
    }
}
