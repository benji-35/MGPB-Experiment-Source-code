using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerStat : NetworkBehaviour
{
    private NetworkManager manager;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Player_Movement move;
    [SerializeField]
    private int startLevel = 0;
    [SerializeField]
    private int maxLife = 100;
    [SerializeField]
    private int startlife = 100;
    [HideInInspector]
    public int lvl;
    [HideInInspector]
    public int life;
    [Header("UI")]
    [SerializeField]
    Text btnLeaveText;
    [SerializeField]
    Text textToggle;
    [SerializeField]
    private GameObject uiPlayer;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Text ipText;
    [SerializeField]
    private Text interractText;
    [SerializeField]
    private Transform spawn;
    [Header("Audio Start")]
    [SerializeField]
    AudioClip clipFr;
    [SerializeField]
    AudioClip clipEn;
    [SerializeField]
    paireAudio[] audios;
    [SerializeField]
    bool[] levelPass;
    private bool pause;
    private bool interract;
    bool onInterract;
    accesLanguage LanguageAcces;
    int idLang;
    // Start is called before the first frame update
    void Start()
    {
        LanguageAcces = GameObject.Find("languageManager").GetComponent<accesLanguage>();
        idLang = PlayerPrefs.GetInt("Language");
        if (idLang == 1)
        {
            GetComponent<AudioSource>().PlayOneShot(clipEn);
        } else
        {
            GetComponent<AudioSource>().PlayOneShot(clipFr);
        }
        spawn = transform;
        pause = false;
        pauseMenu.SetActive(false);
        interractText.gameObject.SetActive(false);
        interractText.text = LanguageAcces.getWordLanguage("interractE");
        textToggle.text = LanguageAcces.getWordLanguage("fullscreen");
        btnLeaveText.text = LanguageAcces.getWordLanguage("leaveMatch");
        if (startlife > maxLife)
            startlife = maxLife;
        life = startlife;
        lvl = startLevel;
        manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        if (manager == null)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("NetworkManager");
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].GetComponent<NetworkManager>() != null)
                {
                    manager = objs[i].GetComponent<NetworkManager>();
                    break;
                }
            }
            if (manager == null)
                return;
        }
        ipText.text = "Ip: " + manager.networkAddress;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            life = 0;
        if (Input.GetKeyDown(KeyCode.Escape) && !onInterract)
        {
            pause = !pause;
            move.enabled = !pause;
            cam.GetComponent<MouseLook>().enabled = !pause;
            pauseMenu.SetActive(pause);
            if (pause)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        interract = false;
        if (Input.GetKey(KeyCode.E))
        {
            interract = true;
        }
        if (life <= 0)
            respawn();
    }

    public void addLevel(int amount)
    {
        this.lvl += amount;
    }

    public void removeLevel(int amount)
    {
        this.lvl -= amount;
    }

    public void removeLife(int amount)
    {
        this.life -= amount;
    }

    public void addLife(int amount)
    {
        this.life += amount;
    }

    public void respawn()
    {
        transform.position.Set(spawn.position.x, spawn.position.y, spawn.position.z);
        life = maxLife;
    }

    public void setSpawner(Transform spawn)
    {
        this.spawn = spawn;
    }

    public Transform getSpawner()
    {
        return this.spawn;
    }

    public void LeaveMatch()
    {
        Debug.Log("Push Button");
        if (!isLocalPlayer)
            return;
        if (manager == null)
            return;
        if (manager.mode == NetworkManagerMode.Host)
        {
            manager.StopHost();
        } else if (manager.mode == NetworkManagerMode.ClientOnly)
        {
            manager.StopClient();
        } else if (manager.mode == NetworkManagerMode.ServerOnly)
        {
            manager.StopServer();
        }
    }

    public Camera getCamPlayer()
    {
        return (this.cam);
    }

    public void rotateCam(Vector3 vect)
    {
        cam.transform.localRotation = Quaternion.Euler(vect);
    }

    public bool getInterract()
    {
        return (interract);
    }

    public void setStatusInterract(bool b)
    {
        interractText.gameObject.SetActive(b);
    }

    public void call_update_doors(door Door, bool stat, console Console, bool callLeave, int lvl)
    {
        if (callLeave)
            Console.leaveConsole();
        if (Door == null)
            return;
        callingDoorUpdate(Door, stat, lvl);
    }

    [Client]
    private void callingDoorUpdate(door Door, bool stat, int lvl)
    {
        CmdchangeStateOpening(stat, Door, lvl);
    }

    [Command]
    public void CmdchangeStateOpening(bool b, door Door, int lvl)
    {
        RpcStatusDoor(b, Door, lvl);
    }

    [ClientRpc]
    void RpcStatusDoor(bool b, door Door, int lvl)
    {
        if (Door != null)
            Door.canOpen = b;
        if (lvl >= levelPass.Length)
            return;
        if (!levelPass[lvl])
        {
            if (idLang == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(audios[lvl].en);
            } else
            {
                GetComponent<AudioSource>().PlayOneShot(audios[lvl].fr);
            }
            levelPass[lvl] = true;
        }
    }

    public void activeCinematic()
    {
        clientActiveCinematic();
    }

    [Client]
    private void clientActiveCinematic()
    {
        CmdCinematic();
    }

    [Command]
    public void CmdCinematic()
    {
        RpcCinematicActive();
    }

    [ClientRpc]
    void RpcCinematicActive()
    {
        move.enabled = false;
        cam.enabled = false;
    }

    public void moveWall(Transform wall, Vector3 target, float speed)
    {
        float mult = speed * Time.deltaTime;
        ClientMoveWall(wall, target, mult);
    }

    [Client]
    private void ClientMoveWall(Transform wall, Vector3 target, float speed)
    {
        CmdMoveWall(wall, target, speed);
    }

    [Command]
    public void CmdMoveWall(Transform wall, Vector3 target, float speed)
    {
        RcpMoveWall(wall, target, speed);
    }

    [ClientRpc]
    void RcpMoveWall(Transform wall, Vector3 target, float multiplyer)
    {
        while (wall.position != target)
            wall.position = Vector3.MoveTowards(wall.position, target, multiplyer);
    }

    public void playMessageAudio(int id)
    {
        if (id >= audios.Length)
            return;
        ClientPlayMessageAudio(id);
    }

    [Client]
    void ClientPlayMessageAudio(int id)
    {
        CmdPlayMessageAudio(id);
    }

    [Command]
    void CmdPlayMessageAudio(int id)
    {
        ClientRPCPlayMessageAudio(id);
    }

    [ClientRpc]
    void ClientRPCPlayMessageAudio(int id)
    {
        if (idLang == 1)
        {
            GetComponent<AudioSource>().PlayOneShot(audios[id].en);
        } else
        {
            GetComponent<AudioSource>().PlayOneShot(audios[id].fr);
        }
    }
    public int getidLang()
    {
        return (idLang);
    }

    public void setOnInteract(bool b)
    {
        this.onInterract = b;
    }
}
