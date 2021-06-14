using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform firePos;

    private TankCategory tankc = TankCategory.Red;
    private float bulletSpeed = 10f;
    private int bulletDamage = 5;
    private bool isEnemy = false;

    private PlayerInput input;

    public float timeBetFire = 1f;
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
        switch (tankc)
        {
            case TankCategory.Blue:
                BulletController bc = BulletManager.GetBullet();
                bc.ResetData(firePos.position, firePos.up, bulletSpeed, bulletDamage, isEnemy);
                break;
            case TankCategory.Red:
                for (int i = 0; i < 2; i++)
                {
                    BulletController bc2 = BulletManager.GetBullet();
                    bc2.ResetData(firePos.position + firePos.right*(i*0.3f-0.15f), firePos.up, bulletSpeed, bulletDamage, isEnemy);
                }
                break;
        }
    }
}
