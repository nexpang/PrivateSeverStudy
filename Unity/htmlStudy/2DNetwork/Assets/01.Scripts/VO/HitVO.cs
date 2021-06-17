using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HitVO
{
    public int socketId;
    public int hp;

    public HitVO(int socketId, int hp)
    {
        this.socketId = socketId;
        this.hp = hp;
    }
}
