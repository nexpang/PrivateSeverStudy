using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadVO
{
    public int socketId;
    public int killerId;

    public DeadVO(int soc, int killerId)
    {
        socketId = soc;
        this.killerId = killerId;
    }
}
