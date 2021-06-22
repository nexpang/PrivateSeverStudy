using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        DeadVO vo = JsonUtility.FromJson<DeadVO>(payload);

        //PlayerRPC rpc = GameManager.instance.GetPlayerRPC(vo.socketId);
        GameManager.DeadPlayerRPC(vo);
    }
}
