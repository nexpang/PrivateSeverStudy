using UnityEngine;
using System;

[Serializable]
public class TransformVo
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 turretRotation;
    public int socketId;
    public TankCategory tank;

    public TransformVo(Vector3 position, Vector3 rotation, Vector3 turretRotation, int socketId, TankCategory tank)
    {
        this.position = position;
        this.rotation = rotation;
        this.turretRotation = turretRotation;
        this.socketId = socketId;
        this.tank = tank;
    }

    public TransformVo()
    {

    }
}
