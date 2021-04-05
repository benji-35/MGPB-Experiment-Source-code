using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerDetector : MonoBehaviour
{
    [SerializeField]
    private Transform spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStat stat = other.GetComponent<PlayerStat>();
        if (stat != null && !spawner.Equals(spawner))
        {
            stat.setSpawner(spawner);
        }
    }
}
