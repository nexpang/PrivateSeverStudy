using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        int socketId = int.Parse(payload);
        //StartCoroutine(RespawnPlayer(socketId));
        GameManager.RespawnPlayerRPC(socketId);
    }
}
