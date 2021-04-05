using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class moveWall : NetworkBehaviour
{
    [SerializeField]
    private Transform p1;
    [SerializeField]
    private Transform p2;
    [SerializeField]
    float speed = 2F;
    bool moving;
    int curr = 0;
    PlayerStat stat;
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (!moving)
            return;
        Vector3 vec = p1.position;
        if (curr == 1)
        {
            vec = p2.position;
        }
        stat.moveWall(this.gameObject.transform, vec, speed);
        moving = false;
    }

    public void changePos(PlayerStat stat)
    {
        if (moving)
            return;
        if (curr == 0)
        {
            curr = 1;
        } else
        {
            curr = 0;
        }
        moving = true;
        this.stat = stat;
    }
}
