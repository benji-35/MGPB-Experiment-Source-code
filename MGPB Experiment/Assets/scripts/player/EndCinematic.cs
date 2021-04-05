using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EndCinematic : NetworkBehaviour
{
    [SerializeField]
    private GameObject cinematicObject;

    private void Awake()
    {
        cinematicObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        other.GetComponent<PlayerStat>().activeCinematic();
        cinematicObject.SetActive(true);
    }
}
