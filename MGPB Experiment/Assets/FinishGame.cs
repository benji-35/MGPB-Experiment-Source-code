using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FinishGame : NetworkBehaviour
{
    NetworkManager manager;
    private void Awake()
    {
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
    }

    public void FINISH_GAME()
    {
        if (!isLocalPlayer)
            return;
        if (manager == null)
            return;
        if (manager.mode == NetworkManagerMode.Host)
        {
            manager.StopHost();
        }
        else if (manager.mode == NetworkManagerMode.ClientOnly)
        {
            manager.StopClient();
        }
        else if (manager.mode == NetworkManagerMode.ServerOnly)
        {
            manager.StopServer();
        }
    }
}
