using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;

public class menuMulti : MonoBehaviour
{
    [SerializeField]
    private InputField inp;
    [SerializeField]
    private NetworkManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Host()
    {
        if (manager == null)
        {
            Debug.LogError("manager is null");
            return;
        }
        manager.StartHost();
    }

    public void Join()
    {
        if (manager == null)
        {
            Debug.LogError("manager is null");
            return;
        }
        manager.networkAddress = inp.text;
        manager.StartClient();
    }

    public void updateIp(string ip)
    {
        if (manager == null)
        {
            Debug.LogError("manager is null");
            return;
        }
        manager.networkAddress = ip;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
