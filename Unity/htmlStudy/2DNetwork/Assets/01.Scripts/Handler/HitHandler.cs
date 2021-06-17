using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        HitVO vo = JsonUtility.FromJson<HitVO>(payload);

        //PlayerRPC rpc = GameManager.instance.GetPlayerRPC(vo.socketId);
        GameManager.RecordHitInfo(vo);
    }
}
