using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class console : NetworkBehaviour
{
    [SerializeField]
    private GameObject screenOn;
    [SerializeField]
    private door Door;
    [SerializeField]
    private string result;
    [SerializeField]
    private InputField answer;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private bool onlyButton;
    [SerializeField]
    private Button cancelButton;
    [SerializeField]
    private Transform posCam;
    [SerializeField]
    Text InputfiledText;
    [SerializeField]
    Text BtnValidText;
    [SerializeField]
    Text BtnLeaveText;
    [SerializeField]
    private int idQuestion;
    PlayerStat stat;
    Player_Movement move;
    GameObject save;
    accesLanguage languageAcces;
    
    // Start is called before the first frame update
    void Start()
    {
        languageAcces = GameObject.Find("languageManager").GetComponent<accesLanguage>();
        screenOn.SetActive(false);
        if (onlyButton)
        {
            answer.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leaveConsole()
    {
        Vector3 new_r = new Vector3(0, 0, 0);
        stat.getCamPlayer().transform.localRotation = Quaternion.Euler(new_r);
        stat.getCamPlayer().transform.localPosition = new Vector3(0, 0.686F, 0);
        stat.getCamPlayer().GetComponent<MouseLook>().enabled = true;
        answer.text = "";
        save.transform.localRotation = Quaternion.Euler(new_r);
        stat.rotateCam(new_r);
        stat.setOnInteract(false);
        move.enabled = true;
        move = null;
        stat = null;
        save = null;
        screenOn.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void checkValid()
    {
        if (onlyButton)
        {
            stat.call_update_doors(Door, true, this, true, idQuestion);
            return;
        }
        if (answer.text == result)
        {
            stat.call_update_doors(Door, true, this, true, idQuestion);
            return;
        }
        else
        {
            stat.call_update_doors(Door, false, this, false, idQuestion);
        }
    }

    void focusCamOnScreen(Transform cam)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cam.position = posCam.position;
        Quaternion lookAt = Quaternion.LookRotation(canvas.transform.position - cam.position);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, lookAt, 0.78F);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stat = other.GetComponent<PlayerStat>();
            if (stat.getInterract())
            {
                updaetText();
                NetworkIdentity id = other.GetComponent<NetworkIdentity>();
                id.AssignClientAuthority(NetworkClient.connection);
                screenOn.SetActive(true);
                stat.setOnInteract(true);
                move = other.GetComponent<Player_Movement>();
                stat.getCamPlayer().GetComponent<MouseLook>().enabled = false;
                save = other.gameObject;
                move.enabled = false;
                canvas.worldCamera = stat.getCamPlayer();
                focusCamOnScreen(stat.getCamPlayer().GetComponent<Transform>());
            } else
            {
                stat.setStatusInterract(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            stat = other.GetComponent<PlayerStat>();
            if (stat.getInterract()) {
                updaetText();
                stat.setStatusInterract(false);
                stat.setOnInteract(true);
                screenOn.SetActive(true);
                move = other.GetComponent<Player_Movement>();
                stat.getCamPlayer().GetComponent<MouseLook>().enabled = false;
                save = other.gameObject;
                move.enabled = false;
                canvas.worldCamera = stat.getCamPlayer();
                focusCamOnScreen(stat.getCamPlayer().GetComponent<Transform>());
            }
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

    void updaetText()
    {
        if (stat == null)
            return;
        InputfiledText.text = languageAcces.getWordLanguage("entercmd");
        BtnValidText.text = languageAcces.getWordLanguage("valid");
        if (onlyButton)
            BtnValidText.text = languageAcces.getWordLanguage("active");
        BtnLeaveText.text = languageAcces.getWordLanguage("leave");
    }
}
