using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHandler : MonoBehaviour, IMsgHandler
{
    private Queue<FireVO> dataQueue = new Queue<FireVO>();
    public object lockObj = new object();

    public void HandleMsg(string payload)
    {
        FireVO vo = JsonUtility.FromJson<FireVO>(payload);
        //Debug.Log(payload);
        lock (lockObj)
        {
            dataQueue.Enqueue(vo);
        }
    }

    void Update()
    {
        lock (lockObj)
        {
            if(dataQueue.Count>0)
            {
                FireVO vo = dataQueue.Dequeue();
                BulletController bc = BulletManager.GetBullet();
                bc.ResetData(vo.pos, vo.direct, vo.speed, vo.damage, true);

                PlayerRPC rpc = GameManager.instance.GetPlayerRPC(vo.socketId);

                rpc.SetTransform(vo.transform.position, vo.transform.rotation, vo.transform.turretRotation);
            }
        }
    }
}