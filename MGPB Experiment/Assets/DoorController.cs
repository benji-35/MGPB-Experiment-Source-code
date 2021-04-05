using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public GameObject Door;
    public bool doorIsOpening;

    void Update () {
        if (doorIsOpening == true) {
            Door.transform.Translate(Vector3.right * Time.deltaTime * 10);
            //if the bool is true open the door
        }
        if (Door.transform.position.x > 1) {
            doorIsOpening = false;
            //if the y of the door is > than 7 we want to stop the door
        }
    }
    void OnMouseDown(){ //THIS FUNCTION WILL DETECT THE MOUSE CLICK ON A COLLIDER,IN OUR CASE WILL DETECT THE CLICK ON THE BUTTON
        doorIsOpening = true;

        //if we click on the button door we must start to open
    }
}
