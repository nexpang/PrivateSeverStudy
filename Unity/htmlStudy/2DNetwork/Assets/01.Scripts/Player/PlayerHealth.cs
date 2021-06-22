using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private PlayerRPC rpc;
    public int maxHP;
    public int currentHP;

    public bool isEnemy = false;
    public InfoUI ui;

    private void Awake()
    {
        rpc = GetComponent<PlayerRPC>();
    }

    public void SetHealthScript(TankDataVO data, bool isEnemy, InfoUI ui)
    {
        maxHP = data.maxHp;
        currentHP = maxHP;
        this.isEnemy = isEnemy;
        this.ui = ui;
    }

    public void OnDamage(int damage, Vector2 powerDir, bool isEnemy)
    {
        if (rpc.isRemote) return;
        currentHP -= damage;
        UpdateUI();
        HitVO vo = new HitVO(GameManager.instance.socketId, currentHP);
        string payload = JsonUtility.ToJson(vo);
        DataVO dataVO = new DataVO();
        dataVO.type = "HIT";
        dataVO.payload = payload;

        SocketClient.SendDataToSocket(JsonUtility.ToJson(dataVO));

        if(currentHP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 체력 게이지 업데이트 해주는 메서드.
    /// </summary>
    public void UpdateUI()
    {
        ui.UpdateHPBar((float)currentHP / (float)maxHP);
    }

    public void Die()
    {
        MassiveExplosion mExp = EffectManager.GetMassiveExplosion();
        mExp.ResetPos(transform.position);

        //gameObject.SetActive(false);
        //ui.gameObject.SetActive(false);

        DeadVO vo = new DeadVO(GameManager.instance.socketId);
        string payload = JsonUtility.ToJson(vo);
        DataVO dataVO = new DataVO();
        dataVO.type = "DEAD";
        dataVO.payload = payload;
        SocketClient.SendDataToSocket(JsonUtility.ToJson(dataVO));

        GameManager.instance.SetPlayerDead();
        rpc.SetScript(false);
    }
}