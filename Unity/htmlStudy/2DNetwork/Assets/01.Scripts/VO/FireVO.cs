using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireVO
{
    public int socketId;
    public Vector3 pos;
    public Vector3 direct;
    public float speed;
    public int damage;

    public TransformVo transform;

    public FireVO(int socketId, Vector3 pos, Vector3 direct, float speed, int damage, TransformVo transform)
    {
        this.socketId = socketId;
        this.pos = pos;
        this.direct = direct;
        this.speed = speed;
        this.damage = damage;
        this.transform = transform;
    }
}
