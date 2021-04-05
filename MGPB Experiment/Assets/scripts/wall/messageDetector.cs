using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class messageDetector : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    GameObject destroyToo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        other.GetComponent<PlayerStat>().playMessageAudio(id);
        if (destroyToo != null)
            destroyToo.SetActive(false);
        gameObject.SetActive(false);
    }
}
