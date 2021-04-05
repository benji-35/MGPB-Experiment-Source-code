using UnityEngine;
using Mirror;

public class playerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] compToDisable;
    [SerializeField]
    private GameObject uiPlayer;
    Camera mainCam;
    void Start()
    {
        if (!isLocalPlayer)
        {
            uiPlayer.SetActive(false);
            for (int i = 0; i < compToDisable.Length; i++)
            {
                compToDisable[i].enabled = false;
            }
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            mainCam = Camera.main;
            if (mainCam != null)
                mainCam.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (mainCam != null)
        {
            mainCam.gameObject.SetActive(true);
        }
    }
}
