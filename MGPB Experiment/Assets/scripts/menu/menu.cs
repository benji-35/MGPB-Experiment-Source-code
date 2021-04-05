using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour {

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveApplication()
    {
        Application.Quit();
    }
}
