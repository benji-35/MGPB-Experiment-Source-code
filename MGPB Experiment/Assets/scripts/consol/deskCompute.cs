using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deskCompute : MonoBehaviour
{
    [SerializeField]
    private GameObject screenLeft;
    [SerializeField]
    private GameObject screenMiddle;
    [SerializeField]
    private GameObject screenRight;
    [SerializeField]
    private GameObject[] RedWalls;
    [SerializeField]
    private GameObject[] GreenWalls;
    [SerializeField]
    private GameObject[] BlueWalls;
    [SerializeField]
    Transform focusPoint;
    [SerializeField]
    Transform camPoint;
    [Header("UI Language")]
    [SerializeField]
    Text[] valid;
    [SerializeField]
    Text[] leave;
    [SerializeField]
    Text blueTitle;
    [SerializeField]
    Text redTitle;
    [SerializeField]
    Text greenTitle;

    Canvas canvasL;
    Canvas canvasM;
    Canvas canvasR;

    PlayerStat stat;
    Player_Movement move;
    GameObject save;
    accesLanguage languageAcces;
    // Start is called before the first frame update
    void Start()
    {
        screenLeft.SetActive(false);
        screenMiddle.SetActive(false);
        screenRight.SetActive(false);

        canvasL = screenLeft.GetComponent<Canvas>();
        canvasM = screenMiddle.GetComponent<Canvas>();
        canvasR = screenRight.GetComponent<Canvas>();

        languageAcces = GameObject.Find("languageManager").GetComponent<accesLanguage>();
        for (int i = 0; i < valid.Length; i++)
            valid[i].text = languageAcces.getWordLanguage("active");
        for (int i = 0; i < leave.Length; i++)
            leave[i].text = languageAcces.getWordLanguage("leave");
        blueTitle.text = languageAcces.getWordLanguage("bluewallcontrol");
        redTitle.text = languageAcces.getWordLanguage("redwallcontrol");
        greenTitle.text = languageAcces.getWordLanguage("greenwallcontrol");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leaveConsole()
    {
        Vector3 new_r = new Vector3(0, 0, 0);
        stat.setOnInteract(false);
        stat.getCamPlayer().transform.localRotation = Quaternion.Euler(new_r);
        stat.getCamPlayer().transform.localPosition = new Vector3(0, 0.686F, 0);
        save.transform.localRotation = Quaternion.Euler(new_r);
        stat.rotateCam(new_r);
        stat.getCamPlayer().GetComponent<MouseLook>().enabled = true;
        move.enabled = true;
        move = null;
        stat = null;
        save = null;
        screenLeft.SetActive(false);
        screenMiddle.SetActive(false);
        screenRight.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void focusCamOnScreen(Transform cam)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cam.position = camPoint.position;
        Quaternion lookAt = Quaternion.LookRotation(focusPoint.position - cam.position);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, lookAt, 0.78F);
    }

    public void PushLeftButton()
    {
        for (int i = 0; i < BlueWalls.Length; i++)
        {
            BlueWalls[i].GetComponent<moveWall>().changePos(stat);
        }
    }

    public void PushMiddleButton()
    {
        for (int i = 0; i < RedWalls.Length; i++)
        {
            RedWalls[i].GetComponent<moveWall>().changePos(stat);
        }
    }

    public void PushRightButton()
    {
        for (int i = 0; i < GreenWalls.Length; i++)
        {
            GreenWalls[i].GetComponent<moveWall>().changePos(stat);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        this.stat = other.GetComponent<PlayerStat>();
        if (stat.getInterract())
        {
            stat.setStatusInterract(false);
            stat.setOnInteract(true);
            screenLeft.SetActive(true);
            screenMiddle.SetActive(true);
            screenRight.SetActive(true);
            move = other.GetComponent<Player_Movement>();
            stat.getCamPlayer().GetComponent<MouseLook>().enabled = false;
            save = other.gameObject;
            move.enabled = false;
            canvasL.worldCamera = stat.getCamPlayer();
            canvasM.worldCamera = stat.getCamPlayer();
            canvasR.worldCamera = stat.getCamPlayer();
            focusCamOnScreen(stat.getCamPlayer().GetComponent<Transform>());
        } else
        {
            stat.setStatusInterract(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerStat>().setStatusInterract(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            this.stat = other.GetComponent<PlayerStat>();
            if (stat.getInterract())
            {
                stat.setStatusInterract(false);
                stat.setOnInteract(true);
                screenLeft.SetActive(true);
                screenMiddle.SetActive(true);
                screenRight.SetActive(true);
                move = other.GetComponent<Player_Movement>();
                stat.getCamPlayer().GetComponent<MouseLook>().enabled = false;
                save = other.gameObject;
                move.enabled = false;
                canvasL.worldCamera = stat.getCamPlayer();
                canvasM.worldCamera = stat.getCamPlayer();
                canvasR.worldCamera = stat.getCamPlayer();
                focusCamOnScreen(stat.getCamPlayer().GetComponent<Transform>());
            }
        }
    }
}
