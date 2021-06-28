using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform firePos;
    public Transform turretTrm;

    private TankCategory tankc = TankCategory.Red;
    private float bulletSpeed = 10f;
    private int bulletDamage = 5;
    private bool isEnemy = false;

    private PlayerInput input;

    public float timeBetFire = 10f;
    public float lastFireTime = 0;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        lastFireTime = Time.time;
    }

    public void SetFireScript(TankDataVO data, bool isEnemy)
    {
        tankc = data.tank;
        bulletSpeed = data.bulletSpeed;
        bulletDamage = data.damage;
        this.isEnemy = isEnemy;
        timeBetFire = data.timeBetFire;
    }

    private void Update()
    {
        if(input.fire && lastFireTime + timeBetFire < Time.time)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }


    public void Fire()
    {
        int socketId = GameManager.instance.socketId;
        switch (tankc)
        {
            case TankCategory.Blue:
                BulletController bc = BulletManager.GetBullet();
                bc.ResetData(socketId,firePos.position, firePos.up, bulletSpeed, bulletDamage, isEnemy);
                SendFireData(firePos.position, firePos.up, bulletSpeed, bulletDamage);
                break;
            case TankCategory.Red:
                for (int i = 0; i < 2; i++)
                {
                    BulletController bc2 = BulletManager.GetBullet();
                    bc2.ResetData(socketId,firePos.position + firePos.right*(i*0.3f-0.15f), firePos.up, bulletSpeed, bulletDamage, isEnemy);
                    SendFireData(firePos.position + firePos.right * (i * 0.3f - 0.15f), firePos.up, bulletSpeed, bulletDamage);
                }
                break;
        }
    }

    private void SendFireData(Vector3 pos, Vector3 direct, float speed, int damage)
    {
        int socketId = GameManager.instance.socketId;
        TransformVo trmVo = new TransformVo(transform.position, transform.eulerAngles, turretTrm.rotation.eulerAngles, socketId, tankc);
        FireVO vo = new FireVO(socketId, pos, direct, speed, damage, trmVo);

        string payload = JsonUtility.ToJson(vo);
        DataVO sendData = new DataVO();
        sendData.type = "FIRE";
        sendData.payload = payload;

        SocketClient.SendDataToSocket(JsonUtility.ToJson(sendData));
    }
}
